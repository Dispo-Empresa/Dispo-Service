using Dispo.Product.Core.Application.Models;

namespace Dispo.Product.Core.Application.Interfaces
{
    public interface IManufacturerService
    {
        long CreateManufacturer(ManufacturerRequestModel manufacturerRequestDto);
    }
}