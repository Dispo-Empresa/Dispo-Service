using Dispo.Domain.Dtos;
using Dispo.Domain.Entities;
using Dispo.Infrastructure.Context;
using Dispo.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Infrastructure.Repositories
{
    public class WarehouseRepository : BaseRepository<Warehouse>, IWarehouseRepository
    {
        private readonly DispoContext dispoContext;
        public WarehouseRepository(DispoContext dispoContext) 
            : base(dispoContext)
        {
            this.dispoContext = dispoContext;
        }

        public IEnumerable<WarehouseAddressDto> GetWarehousesWithAddressByUserId(long userId)
        {
            return dispoContext.UserWarehouses.Include(i => i.Warehouse)
                                              .ThenInclude(i => i.Address)
                                              .Include(i => i.User)
                                              .Where(w => w.UserId == userId)
                                              .Select(s => new WarehouseAddressDto
                                              {
                                                  Address = (s.Warehouse != null && s.Warehouse.Address != null) ? s.Warehouse.Address.ToString() : string.Empty,
                                                  AddressId = (s.Warehouse != null && s.Warehouse.Address != null) ? s.Warehouse.Address.Id : 0,
                                                  Name = (s.Warehouse != null) ? s.Warehouse.Name : string.Empty,
                                                  WarehouseId = s.WarehouseId,
                                                  CurrentWarehouse = s.User.CurrentWarehouseId.HasValue && s.User.CurrentWarehouseId.Value == s.WarehouseId,
                                              });
        }

        public IEnumerable<WarehouseAddressDto> GetWarehousesWithAddress()
        {
            return dispoContext.Warehouses.Include(i => i.Address)
                                          .Select(s => new WarehouseAddressDto
                                          {
                                              Address = (s.Address != null) ? s.Address.ToString() : string.Empty,
                                              AddressId = s.AdressId,
                                              Name = s.Name,
                                              WarehouseId = s.Id,
                                          });
        }
    }
}