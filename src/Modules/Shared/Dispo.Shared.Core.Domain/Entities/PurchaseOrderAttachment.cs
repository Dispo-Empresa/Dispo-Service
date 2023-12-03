namespace Dispo.Shared.Core.Domain.Entities
{
    public class PurchaseOrderAttachment : EntityBase
    {
        public long PurchaseOrderId { get; set; }
        public byte[] Attatchment { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
    }
}