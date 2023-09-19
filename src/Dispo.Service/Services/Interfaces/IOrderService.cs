using Dispo.Domain.DTOs;

namespace Dispo.Service.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrdersWithProductDto>> GetWithProductsAsync();
        Task<List<OrdersWithProductDto>> GetWithProductsByProductIdAsync(long productId);
    }
}
