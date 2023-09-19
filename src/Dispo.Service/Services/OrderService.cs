using Dispo.Domain.DTOs;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;

namespace Dispo.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrdersWithProductDto>> GetWithProductsAsync()
        {
            return await _orderRepository.GetWithProductsAsync();
        }

        public async Task<List<OrdersWithProductDto>> GetWithProductsByProductIdAsync(long productId)
        {
            return await _orderRepository.GetWithProductsByProductIdAsync(productId);
        }
    }
}
