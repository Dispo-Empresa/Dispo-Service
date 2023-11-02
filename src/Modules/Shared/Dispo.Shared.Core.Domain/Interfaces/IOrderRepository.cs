using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<OrdersWithProductDto>> GetWithProductsAsync();

        Task<List<OrdersWithProductDto>> GetWithProductsByProductIdAsync(long productId);

        Task<long> GetProductByOrderId(long orderId);
    }
}