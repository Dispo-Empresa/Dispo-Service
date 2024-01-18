using Dispo.API.ResponseBuilder;
using Dispo.Movement.Core.Application.Interfaces;
using Dispo.Shared.Core.Domain;
using Dispo.Shared.Core.Domain.Interfaces;
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
        private readonly IBatchRepository _batchRepository;

        public BatchesController(IBatchService batchService, IBatchRepository batchRepository)
        {
            _batchService = batchService;
            _batchRepository = batchRepository;
        }

        [HttpGet("get-by-product/{productId}")]
        public IActionResult GetByProduct(long productId)
        {
            var batches = _batchRepository.GetWithQuantityByProduct(productId);
            return Ok(new ResponseModelBuilder().WithData(batches)
                                                .WithSuccess(true)
                                                .Build());
        }

        [HttpGet("get-total-quantity-products/{productId}")]
        public IActionResult GetTotalQuantityOfProducts(long productId)
        {
            var quantity = _batchRepository.GetTotalQuantityOfProduct(productId);
            return Ok(new ResponseModelBuilder().WithData(quantity)
                                                .WithSuccess(true)
                                                .Build());
        }
    }
}