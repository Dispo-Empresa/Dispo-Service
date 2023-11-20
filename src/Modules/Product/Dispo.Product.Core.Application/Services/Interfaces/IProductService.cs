using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.DTOs.Request;
using Microsoft.AspNetCore.Http;

namespace Dispo.Product.Core.Application.Services.Interfaces
{
    public interface IProductService
    {
        long CreateProduct(ProductRequestDto productModel);

        string BuildProductSKUCode(string productName, string productType);

        Task<bool> ExistsByIdAsync(long productId);

        List<ProductInfoDto> GetWithActivePurschaseOrder();
    }
}