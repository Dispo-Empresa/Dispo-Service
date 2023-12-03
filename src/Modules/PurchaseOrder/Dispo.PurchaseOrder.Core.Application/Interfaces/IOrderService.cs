using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.PurchaseOrder.Core.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrdersWithProductDto>> GetWithProductsAsync();

        Task<List<OrdersWithProductDto>> GetWithProductsByProductIdAsync(long productId);
    }
}