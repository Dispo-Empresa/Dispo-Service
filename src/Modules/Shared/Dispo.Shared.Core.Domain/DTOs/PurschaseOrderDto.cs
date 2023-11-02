namespace Dispo.Shared.Core.Domain.DTOs
{
    public class PurschaseOrderDto
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
    }
}