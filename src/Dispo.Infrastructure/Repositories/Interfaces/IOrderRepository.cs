using Dispo.Domain.DTOs;

using Dispo.Domain.Entities;

namespace Dispo.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<OrdersWithProductDto>> GetWithProductsAsync();
        Task<List<OrdersWithProductDto>> GetWithProductsByProductIdAsync(long productId);
    }
}
