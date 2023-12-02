using Dispo.API.ResponseBuilder;
using Dispo.PurchaseOrder.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain;
using Dispo.Shared.Core.Domain.DTOs.Request;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/purchase-orders")]
    [ApiController]
    [Authorize(Roles = $"{Roles.Manager},{Roles.PurchasingManager}")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderController(IPurchaseOrderService purschaseOrderService, IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderService = purschaseOrderService;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PurchaseOrderRequestDto purchaseOrderRequestDto)
        {
            try
            {
                var purchaseOrderCreatedId = _purchaseOrderService.CreatePurchaseOrder(purchaseOrderRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Ordem de compra criada com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithData(purchaseOrderCreatedId)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"Erro inesperado:  {ex.Message}")
                                                            .WithSuccess(false)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
           
            
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var purschaseOrders = _purchaseOrderRepository.GetAll();

            if (purschaseOrders != null)
                purschaseOrders = _purchaseOrderService.FillPurchaseOrderWithSupplier(purschaseOrders);

            return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                .WithData(purschaseOrders)
                                                .Build());
        }

        [HttpGet]
        [Route("get-by-product/{productId}")]
        public IActionResult GetByProcuctId(long productId)
        {
            var purschaseOrders = _purchaseOrderRepository.GetByProcuctId(productId);
            return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                .WithData(purschaseOrders)
                                                .Build());
        }
    }
}