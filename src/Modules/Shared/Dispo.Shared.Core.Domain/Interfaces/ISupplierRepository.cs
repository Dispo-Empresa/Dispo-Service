using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        IEnumerable<SupplierInfoDto> GetSupplierInfoDto();

        long GetSupplierIdByName(string supplierName);
    }
}