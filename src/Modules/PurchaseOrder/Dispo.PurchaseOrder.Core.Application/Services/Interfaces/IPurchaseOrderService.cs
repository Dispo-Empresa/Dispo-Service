using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.DTOs.Request;

namespace Dispo.PurchaseOrder.Core.Application.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        long CreatePurchaseOrder(PurchaseOrderRequestDto supplierRequestDto);

        List<PurschaseOrderDto> GetByProcuctId(long productId);
    }
}