using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<ProductNameWithCode> GetAllProductNames();

        long GetProductIdByName(string productName);

        IEnumerable<ProductInfoDto> GetProductInfoDto();

        IEnumerable<PurchaseOrderInfoDto> GetPurchaseOrderInfoDto();

        List<ProductInfoDto> GetWithActivePurschaseOrder();

        List<ProductExitMovementDto> GetWithSalePrice();
    }
}