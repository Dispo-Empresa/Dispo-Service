using Dispo.Domain.Entities;

namespace Dispo.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(long id);
        User? GetById(long id);
        void LinkWarehouses(List<long> warehouseIds, long userId);
        void ChangeWarehouse(long userId, long warehouseId);
    }
}