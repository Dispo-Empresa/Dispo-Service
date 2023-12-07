using Dispo.Product.Core.Application.Models;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Product.Core.Application.Interfaces
{
    public interface IProductService
    {
        long CreateProduct(ProductRequestModel productModel);
        void UpdateProduct(ProductRequestDto productModel);

        string BuildProductSKUCode(string productName, string productType);

        Task<bool> ExistsByIdAsync(long productId);

        List<ProductInfoDto> GetWithActivePurschaseOrder();
    }
}