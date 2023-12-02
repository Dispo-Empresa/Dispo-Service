using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.DTOs.Request;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.PurchaseOrder.Core.Application.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        long CreatePurchaseOrder(PurchaseOrderRequestDto supplierRequestDto);

        List<PurschaseOrderDto> GetByProcuctId(long productId);

        IEnumerable<Shared.Core.Domain.Entities.PurchaseOrder> FillPurchaseOrderWithSupplier(IEnumerable<Shared.Core.Domain.Entities.PurchaseOrder> purchaseOrderList);
    }
}