using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
    {
        List<PurschaseOrderDto> GetByProcuctId(long productId);
    }
}