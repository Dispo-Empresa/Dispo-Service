using Dispo.PurchaseOrder.Core.Application.Models;

namespace Dispo.PurchaseOrder.Core.Application.Interfaces
{
    public interface IPurchaseOrderService
    {
        long CreatePurchaseOrder(PurchaseOrderRequestModel supplierRequestDto);

        IEnumerable<Shared.Core.Domain.Entities.PurchaseOrder> FillPurchaseOrderWithSupplier(IEnumerable<Shared.Core.Domain.Entities.PurchaseOrder> purchaseOrderList);
    }
}