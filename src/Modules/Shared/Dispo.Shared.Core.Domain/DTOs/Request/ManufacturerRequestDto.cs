using Microsoft.AspNetCore.Http;

namespace Dispo.Shared.Core.Domain.DTOs.Request
{
    public class ManufacturerRequestDto
    {
        public string Name { get; set; }
        public IFormFile? Logo { get; set; }
    }
}