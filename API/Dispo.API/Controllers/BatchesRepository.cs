using Dispo.API.ResponseBuilder;
using Dispo.Movement.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/batches")]
    [ApiController]
    [Authorize(Roles = RolesManager.WarehouseOperatorAssociated)]
    public class BatchesController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchesController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet("get-by-product/{productId}")]
        public IActionResult GetByProduct(long productId)
        {
            var batches = _batchService.GetWithQuantityByProduct(productId);
            return Ok(new ResponseModelBuilder().WithData(batches)
                                                .WithSuccess(true)
                                                .Build());
        }
    }
}