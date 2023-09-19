﻿using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;
using Dispo.Domain.Enums;
using Dispo.Domain.Exceptions;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dispo.Service.Services
{
    public class InputBatchMovementService : IInputBatchMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IBatchMovementService _batchMovementService;
        private readonly IBatchService _batchService;
        private readonly IAccountResolverService _accountResolverService;
        private readonly ILogger<InputBatchMovementService> _logger;

        public InputBatchMovementService(IMovementRepository movementRepository, IBatchMovementService batchMovementService, IBatchService batchService, IAccountResolverService accountResolverService, ILogger<InputBatchMovementService> logger)
        {
            _movementRepository = movementRepository;
            _batchMovementService = batchMovementService;
            _batchService = batchService;
            _accountResolverService = accountResolverService;
            _logger = logger;
        }

        /*
            Movimentação de Entrada
            - Adicionar um Movement em memória.
            - Para cada lote enviado: [
                Criar um BatchMovement.
                Incrementar a Quantity do Movement.
                Decrementar a Quantity do Batch.
            ]
        */
        public async Task MoveAsync(BatchMovimentationDto batchMovimentationDto)
        {
            var movementType = batchMovimentationDto.MovementType;
            var movement = await CreateWithAccountAndWarehouseAsync(movementType);
            foreach (var batchDetails in batchMovimentationDto.Batches)
            {
                // Criar novo lote aqui.
                if (!await _batchService.ExistsByKeyAsync(batchDetails.Key))
                {
                    _logger.LogWarning("Batch with Key {K} do not exists.", batchDetails.Key);
                    continue;
                }

                var batch = new Batch();
                var batchMovementWasCreated = await _batchMovementService.CreateAsync(new BatchMovement
                {
                    BatchId = batch.Id,
                    MovementId = movement.Id
                });

                if (!batchMovementWasCreated)
                {
                    _logger.LogWarning("Batch Movement with Key {K} do could not be created.", batchDetails.Key);
                    continue;
                }

                batch.IncreaseOrDecreaseQuantityByMovementType(movementType, batchDetails.Quantity, batchDetails.ExpirationDate);
                await _batchService.UpdateAsync(batch);
            }
            await _movementRepository.UpdateAsync(movement);
        }

        private async Task<Movement> CreateWithAccountAndWarehouseAsync(eMovementType movementType)
        {
            var accountId = _accountResolverService.GetLoggedAccountId();
            if (accountId is null)
            {
                _logger.LogError("User is not logged-in and the operation could not be completed.");
                throw new UnhandledException("User is not logged-in and the operation could not be completed.");
            }

            var warehouseId = _accountResolverService.GetLoggedWarehouseId();
            if (warehouseId is null)
            {
                _logger.LogError("User don't have a vinculated warehouse.");
                throw new UnhandledException("User don't have a vinculated warehouse.");
            }

            var movement = new Movement
            {
                AccountId = accountId.Value,
                WarehouseId = warehouseId.Value,
                Date = DateTime.UtcNow,
                Type = movementType
            };

            if (!await _movementRepository.CreateAsync(movement))
            {
                _logger.LogError("Movement cannot be created.");
                throw new UnhandledException("Movement cannot be created.");
            }

            return movement;
        }
    }
}
