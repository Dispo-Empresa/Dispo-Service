using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Dispo.Shared.Infrastructure.Persistence.Repositories;
using Dispo.Shared.Utils;

namespace Dispo.PurchaseOrder.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly DispoContext _dispoContext;

        public SupplierRepository(DispoContext dispoContext) : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }

        public IEnumerable<SupplierInfoDto> GetSupplierInfoDto()
            => _dispoContext.Suppliers
                    .Select(s => new SupplierInfoDto()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        ContactName = s.ContactName,
                        Cnpj = s.Cnpj,
                        Email = s.Email,
                        Phone = s.Phone,
                    })
                    .ToList();

        public long GetSupplierIdByName(string supplierName)
            => _dispoContext.Suppliers.Where(x => x.Name == supplierName)
                                          .Select(s => s.Id)
                                          .FirstOrDefault()
                                          .ToLong();
    }
}