namespace Dispo.Shared.Core.Domain.DTOs
{
    public class PurschaseOrderDto
    {
        public long Id { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string SupplierName { get; set; }
        public int OrderQuantity { get; set; }
        public long OrderId { get; set; }
    }
}