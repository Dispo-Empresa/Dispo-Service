using Microsoft.AspNetCore.Http;

namespace Dispo.Product.Core.Application.Models
{
    public class ManufacturerRequestModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Logo { get; set; }
    }
}