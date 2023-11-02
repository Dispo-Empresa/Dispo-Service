namespace Dispo.Shared.Core.Domain.DTOs
{
    public class PurchaseOrderInfoDto
    {
        public long IdOrder { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string PurchaseOrderSupplierName { get; set; }
        public int OrderQuantity { get; set; }
    }
}