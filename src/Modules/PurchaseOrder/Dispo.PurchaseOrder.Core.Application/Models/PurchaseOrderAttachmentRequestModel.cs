using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.PurchaseOrder.Core.Application.Models
{
    public class PurchaseOrderAttachmentRequestModel : EntityBase
    {
        public long PurchaseOrderId { get; set; }
        public byte[] Attatchment { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
    }
}