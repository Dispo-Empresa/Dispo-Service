using Dispo.Domain.DTOs;
using Dispo.Infrastructure.Context;
using Dispo.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DispoContext _context;
        public OrderRepository(DispoContext context)
        {
            _context = context;
        }

        public async Task<List<OrdersWithProductDto>> GetWithProductsAsync()
        {
            return await _context.Orders
                .Include(i => i.Product)
                .Include(i => i.PurchaseOrder.Supplier)
                .Where(w => w.Quantity > 0)
                .Select(s => new OrdersWithProductDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    Quantity = s.Quantity,
                    TotalPrice = s.TotalPrice,
                    Product = new ProductOrderDto
                    {
                        Id = s.Product.Id,
                        Name = s.Product.Name,
                    },
                    PurschaseOrder = new PurchaseOrderDto
                    {
                        Id = s.PurchaseOrder.Id,
                        CreationDate = s.PurchaseOrder.CreationDate,
                        Supplier = new SupplierOrderDto
                        {
                            Id = s.PurchaseOrder.Supplier.Id,
                            Name = s.PurchaseOrder.Supplier.Name
                        }
                    }
                }).ToListAsync();
        }

        public async Task<List<OrdersWithProductDto>> GetWithProductsByProductIdAsync(long productId)
        {
            return await _context.Orders
                .Include(i => i.Product)
                .Include(i => i.PurchaseOrder.Supplier)
                .Where(w => w.Quantity > 0 && w.ProductId == productId)
                .Select(s => new OrdersWithProductDto
                {
                    Id = s.Id,
                    Description = s.Description,
                    Quantity = s.Quantity,
                    TotalPrice = s.TotalPrice,
                    Product = new ProductOrderDto
                    {
                        Id = s.Product.Id,
                        Name = s.Product.Name,
                    },
                    PurschaseOrder = new PurchaseOrderDto
                    {
                        Id = s.PurchaseOrder.Id,
                        CreationDate = s.PurchaseOrder.CreationDate,
                        Supplier = new SupplierOrderDto
                        {
                            Id = s.PurchaseOrder.Supplier.Id,
                            Name = s.PurchaseOrder.Supplier.Name
                        }
                    }
                }).ToListAsync();
        }
    }
}
