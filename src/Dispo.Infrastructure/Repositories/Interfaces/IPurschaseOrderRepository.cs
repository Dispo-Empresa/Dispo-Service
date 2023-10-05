using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;

namespace Dispo.Infrastructure.Repositories.Interfaces
{
    public interface IPurschaseOrderRepository : IBaseRepository<PurchaseOrder>
    {
        List<PurschaseOrderDto> GetByProcuctId(long productId);
    }
}
