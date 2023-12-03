using Dispo.Movement.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;

namespace Dispo.Movement.Core.Application.Services
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