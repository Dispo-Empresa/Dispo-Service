using Dispo.Shared.Core.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Dispo.Shared.Core.Domain.DTOs.Request
{
    public class ProductRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public eProductCategory Category { get; set; }
        public eUnitOfMeasurement UnitOfMeasurement { get; set; }

        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Depth { get; set; }
    }
}