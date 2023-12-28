using Dispo.Movement.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Enums;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;

namespace Dispo.Movement.Core.Application.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;

        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        #region Public Methods

        public async Task<bool> ExistsByKeyAsync(string key)
        {
            return await _batchRepository.ExistsByKeyAsync(key);
        }

        public async Task<bool> UpdateAsync(Batch batch)
        {
            return await _batchRepository.UpdateAsync(batch);
        }

        public List<BatchDetailsDto> GetWithQuantityByProduct(long productId)
        {
            return _batchRepository.GetWithQuantityByProduct(productId);
        }

        public async Task<Batch> GetOrCreateForMovementationAsync(BatchDetailsDto batchDetails, eMovementType movementType)
        {
            if (movementType is eMovementType.Input)
            {
                return await CreateAndIncreaseAsync(batchDetails, movementType);
            }

            return await GetAndDecreaseAsync(batchDetails, movementType);
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<Batch> GetAndDecreaseAsync(BatchDetailsDto batchDetails, eMovementType movementType)
        {
            var batch = await _batchRepository.GetByIdAsync(batchDetails.Id);
            if (batch is null)
            {
                throw new NotFoundException(string.Join("Batch com o Id {0} não foi encontrado.", batchDetails.Id));
            }

            batch.IncreaseOrDecreaseQuantityByMovementType(movementType, batchDetails.Quantity, batchDetails.ExpirationDate);
            await _batchRepository.UpdateAsync(batch);
            return batch;
        }

        private async Task<Batch> CreateAndIncreaseAsync(BatchDetailsDto batchDetails, eMovementType movementType)
        {
            if (batchDetails.OrderId is null)
            {
                throw new UnhandledException("O campo 'OrderId' deve ser preenchido para movimentações de entrada.");
            }

            var batch = new Batch
            {
                Key = batchDetails.Key,
                ManufacturingDate = batchDetails.ManufacturingDate,
                ExpirationDate = batchDetails.ExpirationDate,
                QuantityPerBatch = batchDetails.Quantity,
                OrderId = batchDetails.OrderId.Value,
            };

            batch.IncreaseOrDecreaseQuantityByMovementType(movementType, batchDetails.Quantity, batchDetails.ExpirationDate);
            await _batchRepository.CreateAsync(batch);
            return batch;
        }

        #endregion Private Methods
    }
}