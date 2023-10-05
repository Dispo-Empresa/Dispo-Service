using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;
using Dispo.Domain.Enums;

namespace Dispo.Service.Services.Interfaces
{
    public interface IBatchService
    {
        Task<bool> ExistsByKeyAsync(string key);
        Task<bool> UpdateAsync(Batch batch);
        List<BatchDetailsDto> GetWithQuantityByProduct(long productId);
        Task<Batch> GetOrCreateForMovementationAsync(BatchDetailsDto batchDetails, eMovementType movementType);
    }
}
