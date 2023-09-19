using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;

namespace Dispo.Service.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;
        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

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
    }
}
