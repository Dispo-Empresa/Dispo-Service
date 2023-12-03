using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Dispo.Shared.Utils;
using Dispo.Shared.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Product.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Shared.Core.Domain.Entities.Product>, IProductRepository
    {
        private readonly DispoContext _dispoContext;

        public ProductRepository(DispoContext dispoContext) : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }

        public IEnumerable<ProductNameWithCode> GetAllProductNames()
            => _dispoContext.Products
                            .Select(s => new ProductNameWithCode
                            {
                                Id = s.Id,
                                Name = s.Name,
                            })
                            .Distinct()
                            .ToList();

        public long GetProductIdByName(string productName)
            => _dispoContext.Products
                            .Where(x => x.Name == productName)
                            .Select(s => s.Id)
                            .SingleOrDefault()
                            .ToLong();

        public IEnumerable<ProductInfoDto> GetProductInfoDto()
            => _dispoContext.Products
                            .Select(s => new ProductInfoDto()
                            {
                                Id = s.Id,
                                Name = s.Name,
                                PurchasePrice = s.PurchasePrice.ConvertToCurrency(),
                                SalePrice = s.SalePrice.ConvertToCurrency(),
                                Category = EnumExtension.ConvertToString(s.Category),
                                UnitOfMeasurement = EnumExtension.ConvertToString(s.UnitOfMeasurement),
                            })
                            .ToList();

        public IEnumerable<PurchaseOrderInfoDto> GetPurchaseOrderInfoDto()
            => _dispoContext.Orders.Include(x => x.PurchaseOrder).ThenInclude(x => x.Supplier)
                                   .Select(s => new PurchaseOrderInfoDto()
                                   {
                                       IdOrder = s.Id,
                                       PurchaseOrderNumber = s.PurchaseOrder.Number,
                                       PurchaseOrderDate = s.PurchaseOrder.CreationDate,
                                       PurchaseOrderSupplierName = s.PurchaseOrder.Supplier.Name,
                                       OrderQuantity = s.Quantity
                                   })
                                   .ToList();

        public List<ProductInfoDto> GetWithActivePurschaseOrder()
        {
            return _dispoContext.Products
                                .Include(i => i.Orders).ThenInclude(i => i.PurchaseOrder)
                                .Where(w => w.Orders != null && w.Orders.Any(w => w.PurchaseOrderId > 0))
                                .Select(s => new ProductInfoDto
                                {
                                    Id = s.Id,
                                    Name = s.Name
                                }).ToList();
        }

        public List<ProductExitMovementDto> GetWithSalePrice()
        {
            return _dispoContext.Products.Select(s => new ProductExitMovementDto
            {
                Id = s.Id,
                Name = s.Name,
                SalePrice = s.SalePrice
            }).ToList();
        }
    }
}