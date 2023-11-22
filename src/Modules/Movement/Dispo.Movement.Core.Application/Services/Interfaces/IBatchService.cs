using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Enums;

namespace Dispo.Movement.Core.Application.Services.Interfaces
{
    public interface IBatchService
    {
        Task<bool> ExistsByKeyAsync(string key);

        Task<bool> UpdateAsync(Batch batch);

        List<BatchDetailsDto> GetWithQuantityByProduct(long productId);

        Task<Batch> GetOrCreateForMovementationAsync(BatchDetailsDto batchDetails, eMovementType movementType);
    }
}