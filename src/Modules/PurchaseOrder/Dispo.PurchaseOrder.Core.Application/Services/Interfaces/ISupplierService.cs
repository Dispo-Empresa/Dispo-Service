using Dispo.Shared.Core.Domain.DTOs.Request;

namespace Dispo.PurchaseOrder.Core.Application.Services.Interfaces
{
    public interface ISupplierService
    {
        long CreateSupplier(SupplierRequestDto supplierRequestDto);
    }
}