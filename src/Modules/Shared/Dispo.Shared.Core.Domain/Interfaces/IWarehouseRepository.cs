using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IWarehouseRepository : IBaseRepository<Warehouse>
    {
        IEnumerable<WarehouseAddressDto> GetWarehousesWithAddressByAccountId(long accountId);

        IEnumerable<WarehouseAddressDto> GetWarehousesWithAddress();

        IEnumerable<WarehouseInfoDto> GetWarehouseInfo();

        string GetNameById(long id);

        bool ExistsByAddressId(long addressId);
    }
}