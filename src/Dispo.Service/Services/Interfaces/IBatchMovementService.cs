using Dispo.Domain.Entities;

namespace Dispo.Service.Services.Interfaces
{
    public interface IBatchMovementService
    {
        Task<bool> CreateAsync(BatchMovement batchMovement);
    }
}
