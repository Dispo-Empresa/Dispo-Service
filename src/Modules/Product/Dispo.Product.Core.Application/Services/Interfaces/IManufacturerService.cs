using Dispo.Shared.Core.Domain.DTOs.Request;

namespace Dispo.Product.Core.Application.Services.Interfaces
{
    public interface IManufacturerService
    {
        long CreateManufacturer(ManufacturerRequestDto manufacturerRequestDto);
    }
}