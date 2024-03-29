﻿using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Movement.Core.Application.Interfaces;
using Dispo.Product.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Enums;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Log;
using System.Transactions;

namespace Dispo.Movement.Core.Application.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IProductService _productService;
        private readonly IAccountResolverService _accountResolverService;
        private readonly IBatchService _batchService;
        private readonly IBatchMovementService _batchMovementService;
        private readonly ILoggingService _logger;

        public MovementService(IMovementRepository movementRepository, IProductService productService, IAccountResolverService accountResolverService, IBatchService batchService, IBatchMovementService batchMovementService, ILoggingService logger)
        {
            _movementRepository = movementRepository;
            _productService = productService;
            _accountResolverService = accountResolverService;
            _batchService = batchService;
            _batchMovementService = batchMovementService;
            _logger = logger;
        }

        #region Public Methods

        /// <summary>
        /// Realiza a movimentação de um produto.
        /// </summary>
        /// <param name="productMovimentationDto"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="UnhandledException"></exception>
        public async Task MoveProductAsync(ProductMovimentationDto productMovimentationDto)
        {
            _logger.Information("Iniciando uma movimentação do produto {P} no depósito {I}.", productMovimentationDto.ProductId, productMovimentationDto.WarehouseId);

            await ValidateProductExistenceAsync(productMovimentationDto.ProductId);

            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await CreateMovementAsync(productMovimentationDto);
                await UpdateProductWarehouseQuantityAsync(productMovimentationDto);

                ts.Complete();
            }

            _logger.Information("Movimentação do produto {P} no depósito {I} realizada.", productMovimentationDto.ProductId, productMovimentationDto.WarehouseId);
        }

        public async Task MoveBatchAsync(BatchMovimentationDto batchMovimentationDto)
        {
            if (batchMovimentationDto.MovementType == eMovementType.Input && batchMovimentationDto.Batches.Any(x => _movementRepository.ExistsInputMovementByOrderId(x.OrderId.Value)))
            {
                throw new BusinessException("Já existe movimentação de entrada para esta ordem de compra");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var movement = await CreateWithAccountAndWarehouseByMovementTypeAsync(batchMovimentationDto.MovementType);
                foreach (var batchDetails in batchMovimentationDto.Batches)
                {
                    if (batchMovimentationDto.MovementType is eMovementType.Input && await _batchService.ExistsByKeyAsync(batchDetails.Key))
                    {
                        _logger.Warning("Batch com a Key {K} já existe.", batchDetails.Key);
                        continue;
                    }

                    movement.Quantity += batchDetails.Quantity;
                    var batch = await _batchService.GetOrCreateForMovementationAsync(batchDetails, batchMovimentationDto.MovementType);
                    await _batchMovementService.CreateAsync(new BatchMovement
                    {
                        BatchId = batch.Id,
                        MovementId = movement.Id
                    });
                }
                await _movementRepository.UpdateAsync(movement);
                transaction.Complete();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<Shared.Core.Domain.Entities.Movement> CreateWithAccountAndWarehouseByMovementTypeAsync(eMovementType movementType)
        {
            var accountId = _accountResolverService.GetLoggedAccountId();
            if (accountId is null)
            {
                _logger.Error("O usuário não está autenticado.");
                throw new UnhandledException("O usuário não está autenticado.");
            }

            var warehouseId = _accountResolverService.GetLoggedWarehouseId();
            if (warehouseId is null)
            {
                _logger.Error("O usuário não possui um estoque vinculado.");
                throw new UnhandledException("O usuário não possui um estoque vinculado.");
            }

            var movement = new Shared.Core.Domain.Entities.Movement
            {
                AccountId = accountId.Value,
                WarehouseId = warehouseId.Value,
                Date = DateTime.UtcNow,
                Type = movementType
            };

            if (!await _movementRepository.CreateAsync(movement))
            {
                _logger.Error("Não foi possível criar a movimentação.");
                throw new UnhandledException("Não foi possível criar a movimentação.");
            }

            return movement;
        }

        /// <summary>
        /// Validate if a Product exists by its Id.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        private async Task ValidateProductExistenceAsync(long productId)
        {
            var productExists = await _productService.ExistsByIdAsync(productId);
            if (!productExists)
            {
                _logger.Error("Produto com o Id {P} não foi encontrado.", productId);
                throw new NotFoundException($"Produto com o Id {productId} não foi encontrado.");
            }
        }

        /// <summary>
        /// Creates a new movimentation
        /// </summary>
        /// <param name="productMovimentationDto"></param>
        /// <returns></returns>
        /// <exception cref="UnhandledException"></exception>
        private async Task CreateMovementAsync(ProductMovimentationDto productMovimentationDto)
        {
            var createdMovimentation = await _movementRepository.CreateAsync(new Shared.Core.Domain.Entities.Movement
            {
                Date = DateTime.UtcNow,
                //ProductId = productMovimentationDto.ProductId,
                Quantity = productMovimentationDto.Quantity,
                Type = productMovimentationDto.MovementType,
            });

            if (!createdMovimentation)
            {
                _logger.Error("Movimentação não pode ser criada.");
                throw new UnhandledException("Movimentação não pode ser criada.");
            }
        }

        /// <summary>
        /// Update Product quantity in the Warehouse it's located.
        /// </summary>
        /// <param name="productMovimentationDto"></param>
        /// <returns></returns>
        /// <exception cref="UnhandledException"></exception>
        private async Task UpdateProductWarehouseQuantityAsync(ProductMovimentationDto productMovimentationDto)
        {
            //var updatedQuantity = await _productWarehouseQuantityService.UpdateProductWarehouseQuantityAsync(productMovimentationDto);
            //if (!updatedQuantity)
            //{
            //    _logger.LogError("Quantidade não pode ser atualizada.");
            //    throw new UnhandledException("Quantidade não pode ser atualizada.");
            //}
        }

        #endregion Private Methods
    }
}