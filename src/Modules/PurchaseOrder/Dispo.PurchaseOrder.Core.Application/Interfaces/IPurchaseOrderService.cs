using Dispo.PurchaseOrder.Core.Application.Models;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.PurchaseOrder.Core.Application.Interfaces
{
    public interface IPurchaseOrderService
    {
        long CreatePurchaseOrder(PurchaseOrderRequestModel supplierRequestDto);

        List<PurschaseOrderDto> GetByProcuctId(long productId);
    }
}