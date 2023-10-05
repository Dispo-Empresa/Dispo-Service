using Dispo.Domain.DTOs;
using Dispo.Domain.DTOs.Request;

namespace Dispo.Service.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        long CreatePurchaseOrder(PurchaseOrderRequestDto supplierRequestDto);
        List<PurschaseOrderDto> GetByProcuctId(long productId);
    }
}
