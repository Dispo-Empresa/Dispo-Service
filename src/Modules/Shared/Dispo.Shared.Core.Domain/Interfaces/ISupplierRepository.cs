using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        IEnumerable<SupplierInfoDto> GetSupplierInfoDto();

        long GetSupplierIdByName(string supplierName);
    }
}