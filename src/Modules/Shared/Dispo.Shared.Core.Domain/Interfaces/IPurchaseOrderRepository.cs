using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
    {
        List<PurschaseOrderDto> GetByProcuctId(long productId);
    }
}