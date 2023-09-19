using Dispo.API.ResponseBuilder;
using Dispo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/purschase-orders")]
    [ApiController]
    [Authorize]
    public class PurschaseOrderController : ControllerBase
    {
        private readonly IPurschaseOrderService _purschaseOrderService;

        public PurschaseOrderController(IPurschaseOrderService purschaseOrderService)
        {
            _purschaseOrderService = purschaseOrderService;
        }

        [HttpGet]
        [Route("get-by-product/{productId}")]
        public IActionResult GetByProcuctId(long productId)
        {
            var purschaseOrders = _purschaseOrderService.GetByProcuctId(productId);
            return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                .WithData(purschaseOrders)
                                                .Build());
        }
    }
}