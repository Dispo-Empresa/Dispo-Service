using Dispo.PurchaseOrder.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Interfaces;

namespace Dispo.PurchaseOrder.Core.Application.Services
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