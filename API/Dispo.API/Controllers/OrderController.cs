using Dispo.PurchaseOrder.Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("get-with-products")]
        public async Task<IActionResult> GetWithProductsAsync()
        {
            var orders = await _orderService.GetWithProductsAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("get-with-products/{productId}")]
        public async Task<IActionResult> GetWithProductsByProductIdAsync(long productId)
        {
            var orders = await _orderService.GetWithProductsByProductIdAsync(productId);
            return Ok(orders);
        }
    }
}