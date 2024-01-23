using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IWarehouseRepository : IBaseRepository<Warehouse>
    {
        IEnumerable<WarehouseAddressDto> GetWarehousesWithAddressByAccountId(long accountId);

        IEnumerable<WarehouseAddressDto> GetWarehousesWithAddress();

        IEnumerable<WarehouseInfoDto> GetWarehouseInfo();

        string GetNameById(long id);

        IEnumerable<WarehouseInfoDto> GetWarehousesByAccountId(long accountId);
    }
}