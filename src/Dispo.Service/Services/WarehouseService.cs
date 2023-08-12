using Dispo.Domain.Dtos;
using Dispo.Domain.Entities;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;

namespace Dispo.Service.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }

        public void Create(Warehouse entity)
        {
            warehouseRepository.Create(entity);
        }

        public IList<WarehouseAddressDto> GetWarehousesWithAddressByUserId(long userId)
        {
            return warehouseRepository.GetWarehousesWithAddressByUserId(userId)
                                      .ToList();
        }

        public IList<WarehouseAddressDto> GetWarehousesWithAddress()
        {
            return warehouseRepository.GetWarehousesWithAddress()
                                      .ToList();
        }
    }
}
