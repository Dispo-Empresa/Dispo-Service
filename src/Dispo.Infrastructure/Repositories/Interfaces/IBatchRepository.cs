using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;

namespace Dispo.Infrastructure.Repositories.Interfaces
{
    public interface IBatchRepository : IBaseRepository<Batch>
    {
        Task<bool> ExistsByKeyAsync(string key);
        List<BatchDetailsDto> GetWithQuantityByProduct(long productId);
        Task<Batch?> GetByKeyAsync(string key);
    }
}
