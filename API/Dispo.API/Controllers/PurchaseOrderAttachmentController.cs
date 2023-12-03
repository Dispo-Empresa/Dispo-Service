using Dispo.API.ResponseBuilder;
using Dispo.PurchaseOrder.Core.Application.Interfaces;
using Dispo.PurchaseOrder.Core.Application.Models;
using Dispo.Shared.Core.Domain;
using Dispo.Shared.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/purchaseorderattachment")]
    [ApiController]
    [Authorize(Roles = RolesManager.PurchasingManagerAssociated)]
    public class PurchaseOrderAttachmentController : ControllerBase
    {
        private readonly IPurchaseOrderAttachmentService _purchaseOrderAttachmentService;

        public PurchaseOrderAttachmentController(IPurchaseOrderAttachmentService purchaseOrderAttachmentService)
        {
            _purchaseOrderAttachmentService = purchaseOrderAttachmentService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PurchaseOrderAttachmentRequestModel purchaseOrderAttachmentRequestDto)
        {
            try
            {
                var purchaseOrderAttachmentCreateId = _purchaseOrderAttachmentService.CreatePurchaseOrderAttachment(purchaseOrderAttachmentRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Anexo de ordem de compra criado com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithData(purchaseOrderAttachmentCreateId)
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
    }
}