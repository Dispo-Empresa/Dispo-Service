using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dispo.Product.Infrastructure.Persistence.Repositories
{
    public class WarehouseRepository : BaseRepository<Warehouse>, IWarehouseRepository
    {
        private readonly DispoContext _dispoContext;

        public WarehouseRepository(DispoContext dispoContext)
            : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }

        #region Expressions

        #endregion Expressions

        public IEnumerable<WarehouseAddressDto> GetWarehousesWithAddressByAccountId(long accountId)
            => _dispoContext.WarehouseAccounts.Include(i => i.Warehouse)
                                              .Where(w => w.AccountId == accountId)
                                              .Include(i => i.Account)
                                              .Select(s => new WarehouseAddressDto
                                              {
                                                  Name = s.Warehouse != null ? s.Warehouse.Name : string.Empty,
                                                  WarehouseId = s.WarehouseId,
                                                  CurrentWarehouse = s.Account.CurrentWarehouseId.HasValue && s.Account.CurrentWarehouseId.Value == s.WarehouseId,
                                              })
                                              .ToList();

        public IEnumerable<WarehouseAddressDto> GetWarehousesWithAddress()
            => _dispoContext.Warehouses.Select(s => new WarehouseAddressDto
                                        {
                                            Name = s.Name,
                                            WarehouseId = s.Id,
                                        })
                                        .ToList();

        public IEnumerable<WarehouseInfoDto> GetWarehouseInfo()
            => _dispoContext.Warehouses.Select(s => new WarehouseInfoDto
            {
                WarehouseId = s.Id,
                Name = s.Name
            })
                                       .ToList();

        public string GetNameById(long id)
            => _dispoContext.Warehouses.Where(x => x.Id == id)
                                       .Select(s => s.Name)
                                       .FirstOrDefault() ?? string.Empty;

        public IEnumerable<WarehouseInfoDto> GetWarehousesByAccountId(long accountId)
            => _dispoContext.WarehouseAccounts.Include(x => x.Warehouse)
                                              .Include(x => x.Account)
                                              .Where(x => x.AccountId == accountId)
                                              .Select(s => new WarehouseInfoDto
                                              {
                                                  WarehouseId = s.WarehouseId,
                                                  Name = s.Warehouse.Name
                                              })
                                              .ToList();
    }
}