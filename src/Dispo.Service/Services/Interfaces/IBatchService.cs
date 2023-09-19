using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;

namespace Dispo.Service.Services.Interfaces
{
    public interface IBatchService
    {
        Task<bool> ExistsByKeyAsync(string key);
        Task<bool> UpdateAsync(Batch batch);
        List<BatchDetailsDto> GetWithQuantityByProduct(long productId);
    }
}
