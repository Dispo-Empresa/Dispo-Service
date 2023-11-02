using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IBatchRepository : IBaseRepository<Batch>
    {
        Task<bool> ExistsByKeyAsync(string key);

        List<BatchDetailsDto> GetWithQuantityByProduct(long productId);

        Task<Batch?> GetByKeyAsync(string key);
    }
}