using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<ProductNameWithCode> GetAllProductNames();

        long GetProductIdByName(string productName);

        IEnumerable<ProductInfoDto> GetProductInfoDto();

        IEnumerable<PurchaseOrderInfoDto> GetPurchaseOrderInfoDto();

        List<ProductMovementDto> GetWithActivePurschaseOrderByMovementType(int movementType);

        List<ProductExitMovementDto> GetWithSalePrice();
    }
}