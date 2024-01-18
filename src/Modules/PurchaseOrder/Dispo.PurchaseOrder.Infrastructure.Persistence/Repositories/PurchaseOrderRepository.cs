using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Enums;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Dispo.PurchaseOrder.Infrastructure.Persistence.Repositories
{
    public class PurchaseOrderRepository : BaseRepository<Shared.Core.Domain.Entities.PurchaseOrder>, IPurchaseOrderRepository
    {
        private readonly DispoContext _context;

        public PurchaseOrderRepository(DispoContext context) : base(context)
        {
            _context = context;
        }

        public List<PurschaseOrderDto> GetByProcuctId(long productId)
        {
            return _context.PurchaseOrders
                           .Include(i => i.Supplier)
                           .Include(i => i.Orders)
                           .Where(w => w.Orders != null && w.Orders.Any(o => o.ProductId == productId))
                           .Select(s => new PurschaseOrderDto
                           {
                               Id = s.Id,
                               PurchaseOrderNumber = s.Number,
                               PurchaseOrderDate = s.CreationDate,
                               OrderQuantity = s.Orders.Where(w => w.ProductId == productId).Sum(s => s.Quantity),
                               SupplierName = s.Supplier.Name,
                               OrderId = s.Orders.Where(w => w.PurchaseOrderId == s.Id).Select(s => s.Id).First()
                           }).ToList();
        }
    }
}