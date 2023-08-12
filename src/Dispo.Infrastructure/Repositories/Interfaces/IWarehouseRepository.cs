using Dispo.Domain.Dtos;
using Dispo.Domain.Entities;

namespace Dispo.Infrastructure.Repositories.Interfaces
{
    public interface IWarehouseRepository : IBaseRepository<Warehouse>
    {
        IEnumerable<WarehouseAddressDto> GetWarehousesWithAddressByUserId(long userId);
        IEnumerable<WarehouseAddressDto> GetWarehousesWithAddress();
    }
}