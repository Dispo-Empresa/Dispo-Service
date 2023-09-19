using Dispo.Domain.Entities;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;

namespace Dispo.Service.Services
{
    public class BatchMovementService : IBatchMovementService
    {
        private readonly IBatchMovementRepository _batchMovementRepository;
        public BatchMovementService(IBatchMovementRepository batchMovementRepository)
        {
            _batchMovementRepository = batchMovementRepository;
        }

        public async Task<bool> CreateAsync(BatchMovement batchMovement)
        {
            return await _batchMovementRepository.CreateAsync(batchMovement);
        }
    }
}
