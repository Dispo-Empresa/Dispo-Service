using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Product.Core.Application.Services.Interfaces
{
    public interface IAddressService
    {
        IList<WarehouseAddressDto> GetFormattedAddresses();
    }
}