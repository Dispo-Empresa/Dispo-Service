namespace Dispo.Shared.Core.Domain.Entities
{
    public class ProductManufacturer : EntityBase
    {
        public long ProductId { get; set; }
        public long ManufacturerId { get; set; }

        public Product Product { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}