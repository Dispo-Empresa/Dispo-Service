using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;
using Dispo.Infrastructure.Context;
using Dispo.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        private readonly DispoContext _context;
        public PurchaseOrderRepository(DispoContext context) : base(context)
        {
            this._context = context;
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
                               CreationDate = s.CreationDate,
                               Quantity = s.Orders.Where(w => w.ProductId == productId).Sum(s => s.Quantity),
                               Supplier = s.Supplier.Name
                           }).ToList();
        }
    }
}
