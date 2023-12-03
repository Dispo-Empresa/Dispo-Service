using Dispo.Product.Core.Application.Models;

namespace Dispo.Product.Core.Application.Interfaces
{
    public interface IWarehouseService
    {
        void CreateWarehouse(WarehouseRequestModel warehouseRequestDto);
    }
}