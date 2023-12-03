using Dispo.PurchaseOrder.Core.Application.Models;

namespace Dispo.PurchaseOrder.Core.Application.Interfaces
{
    public interface IPurchaseOrderAttachmentService
    {
        long CreatePurchaseOrderAttachment(PurchaseOrderAttachmentRequestModel PurchaseOrderAttachment);
    }
}