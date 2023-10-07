using Dispo.Domain.DTO_s;
using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;

namespace Dispo.Infrastructure.Repositories.Interfaces
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