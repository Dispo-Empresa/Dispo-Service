using Dispo.Domain.Dtos;
using Dispo.Domain.Entities;

namespace Dispo.Service.Services.Interfaces
{
   public interface IWarehouseService
   {
        void Create(Warehouse entity);
        IList<WarehouseAddressDto> GetWarehousesWithAddressByUserId(long userId);
        IList<WarehouseAddressDto> GetWarehousesWithAddress();
   }
}
