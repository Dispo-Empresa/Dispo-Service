using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Dispo.PurchaseOrder.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly DispoContext _context;

        public OrderRepository(DispoContext context)
            : base(context)
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

        public Task<long> GetProductByOrderId(long orderId)
        {
            return _context.Orders.Where(w => w.Id == orderId)
                                  .Select(s => s.ProductId)
                                  .FirstOrDefaultAsync();
        }
    }
}