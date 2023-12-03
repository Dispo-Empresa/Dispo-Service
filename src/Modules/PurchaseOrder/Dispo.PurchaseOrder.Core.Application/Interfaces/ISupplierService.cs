using Dispo.PurchaseOrder.Core.Application.Models;

namespace Dispo.PurchaseOrder.Core.Application.Interfaces
{
    public interface ISupplierService
    {
        long CreateSupplier(SupplierRequestModel supplierRequestDto);
    }
}