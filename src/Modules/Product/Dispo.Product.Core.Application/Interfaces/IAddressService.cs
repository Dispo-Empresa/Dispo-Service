using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Product.Core.Application.Interfaces
{
    public interface IAddressService
    {
        IList<WarehouseAddressDto> GetFormattedAddresses();
    }
}