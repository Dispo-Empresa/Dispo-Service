using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;
using Dispo.Domain.Exceptions;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dispo.Service.Services
{
    public class OutputBatchMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IBatchMovementService _batchMovementService;
        private readonly IBatchService _batchService;
        private readonly IAccountResolverService _accountResolverService;
        private readonly ILogger<InputBatchMovementService> _logger;

        public OutputBatchMovementService(IMovementRepository movementRepository, IBatchMovementService batchMovementService, IBatchService batchService, IAccountResolverService accountResolverService, ILogger<InputBatchMovementService> logger)
        {
            _movementRepository = movementRepository;
            _batchMovementService = batchMovementService;
            _batchService = batchService;
            _accountResolverService = accountResolverService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new input batch movimentation.
        /// </summary>
        /// <param name="batchMovimentationDto"></param>
        /// <returns></returns>
        public async Task MoveAsync(BatchMovimentationDto batchMovimentationDto)
        {
            var movement = await CreateWithAccountAndWarehouseAsync(batchMovimentationDto);
            //foreach (var batch in batchMovimentationDto.Batches)
            //{
            //    var batchInstance = await _batchService.ExistsByKeyAsync(batch.Key);
            //    if (batchInstance is null)
            //    {
            //        _logger.LogWarning("Batch with Key {K} do not exists.", batch.Key);
            //        continue;
            //    }

            //    var batchMovementWasCreated = await _batchMovementService.CreateAsync(new BatchMovement
            //    {
            //        BatchId = batchInstance.Id,
            //        MovementId = movement.Id
            //    });

            //    if (!batchMovementWasCreated)
            //    {
            //        _logger.LogWarning("Batch Movement with Key {K} do could not be created.", batch.Key);
            //        continue;
            //    }

            //    movement.Quantity += batch.Quantity;
            //    batchInstance.QuantityPerBatch -= batch.Quantity;
            //    batchInstance.ExpirationDate = batch.ExpirationDate.GetValueOrDefault(DateTime.UtcNow);
            //    await _batchService.UpdateAsync(batchInstance);
            //}
            await _movementRepository.UpdateAsync(movement);
        }

        private async Task<Movement> CreateWithAccountAndWarehouseAsync(BatchMovimentationDto batchMovimentationDto)
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
                Type = batchMovimentationDto.MovementType
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
