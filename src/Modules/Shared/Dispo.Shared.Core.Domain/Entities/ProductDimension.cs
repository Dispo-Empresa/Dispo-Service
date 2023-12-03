namespace Dispo.Shared.Core.Domain.Entities
{
    public class ProductDimension : EntityBase
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }

        public Product Product { get; set; }
    }
}