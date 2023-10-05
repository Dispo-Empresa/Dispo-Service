namespace Dispo.Domain.DTOs
{
    public class OrdersWithProductDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductOrderDto Product { get; set; }
        public PurchaseOrderDto PurschaseOrder { get; set; }
    }

    public class ProductOrderDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class PurchaseOrderDto
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public SupplierOrderDto Supplier { get; set; }
    }

    public class SupplierOrderDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
