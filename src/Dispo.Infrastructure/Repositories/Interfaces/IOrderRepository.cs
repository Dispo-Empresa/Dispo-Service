using Dispo.Domain.DTOs;

namespace Dispo.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrdersWithProductDto>> GetWithProductsAsync();
    }
}
