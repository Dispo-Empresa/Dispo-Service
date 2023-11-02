using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.DTOs.Request
{
    public class PurchaseOrderAttachmentRequestDto : Base
    {
        public long PurchaseOrderId { get; set; }
        public byte[] Attatchment { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
    }
}