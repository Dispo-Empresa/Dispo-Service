using Dispo.Shared.Core.Domain.DTOs.Request;

namespace Dispo.PurchaseOrder.Core.Application.Services.Interfaces
{
    public interface IPurchaseOrderAttachmentService
    {
        long CreatePurchaseOrderAttachment(PurchaseOrderAttachmentRequestDto PurchaseOrderAttachment);
    }
}