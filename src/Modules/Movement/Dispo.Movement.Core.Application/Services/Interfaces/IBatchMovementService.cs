using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Movement.Core.Application.Services.Interfaces
{
    public interface IBatchMovementService
    {
        Task<bool> CreateAsync(BatchMovement batchMovement);
    }
}