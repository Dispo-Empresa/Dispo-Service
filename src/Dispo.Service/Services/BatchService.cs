using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;
using Dispo.Domain.Enums;
using Dispo.Domain.Exceptions;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;
using System.Threading.Tasks.Dataflow;

namespace Dispo.Service.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;
        private readonly IOrderRepository _orderRepository;
        public BatchService(IBatchRepository batchRepository, IOrderRepository orderRepository)
        {
            _batchRepository = batchRepository;
            _orderRepository = orderRepository;
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

        #endregion

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

            var productId = await _orderRepository.GetProductByOrderId(batchDetails.OrderId.Value);
            var batch = new Batch
            {
                Key = batchDetails.Key,
                ManufacturingDate = batchDetails.ManufacturingDate.HasValue ? batchDetails.ManufacturingDate.Value : DateTime.Now,
                ExpirationDate = batchDetails.ExpirationDate.HasValue ? batchDetails.ExpirationDate.Value : DateTime.Now,
                QuantityPerBatch = batchDetails.Quantity,
                OrderId = batchDetails.OrderId.Value,
                ProductId = productId,
            };
            batch.IncreaseOrDecreaseQuantityByMovementType(movementType, batchDetails.Quantity, batchDetails.ExpirationDate);
            await _batchRepository.CreateAsync(batch);
            return batch;
        }

        #endregion
    }
}
