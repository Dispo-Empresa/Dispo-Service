using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;

namespace Dispo.Product.Infrastructure.Persistence.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly DispoContext dispoContext;

        public AddressRepository(DispoContext dispoContext)
            : base(dispoContext)
        {
            this.dispoContext = dispoContext;
        }

        public IEnumerable<WarehouseAddressDto> GetFormattedAddresses()
        {
            return dispoContext.Addresses.Select(w => new WarehouseAddressDto
            {
                AddressId = w.Id,
                Address = w.ToString(),
            });
        }
    }
}