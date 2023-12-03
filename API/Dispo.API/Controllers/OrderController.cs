using Dispo.PurchaseOrder.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/orders")]
    [ApiController]
    [Authorize(Roles = RolesManager.PurchasingManagerAssociated)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-with-products")]
        public async Task<IActionResult> GetWithProductsAsync()
        {
            var orders = await _orderService.GetWithProductsAsync();
            return Ok(orders);
        }

        [HttpGet("get-with-products/{productId}")]
        public async Task<IActionResult> GetWithProductsByProductIdAsync(long productId)
        {
            var orders = await _orderService.GetWithProductsByProductIdAsync(productId);
            return Ok(orders);
        }
    }
}