using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Product.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Product.Core.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public IList<WarehouseAddressDto> GetFormattedAddresses()
        {
            return addressRepository.GetFormattedAddresses()
                                    .ToList();
        }
    }
}