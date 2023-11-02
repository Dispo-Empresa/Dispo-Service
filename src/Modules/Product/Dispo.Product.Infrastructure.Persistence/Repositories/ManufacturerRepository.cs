using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Dispo.Shared.Infrastructure.Persistence.Repositories;
using Dispo.Shared.Utils;

namespace Dispo.Product.Infrastructure.Persistence.Repositories
{
    public class ManufacturerRepository : BaseRepository<Manufacturer>, IManufacturerRepository
    {
        private readonly DispoContext _dispoContext;

        public ManufacturerRepository(DispoContext dispoContext) : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }

        public IEnumerable<ManufacturerInfoDto> GetManufacturerInfoDto()
            => _dispoContext.Manufacturers
                            .Select(s => new ManufacturerInfoDto()
                            {
                                Id = s.Id,
                                Name = s.Name,
                            })
                            .ToList();

        public long GetManufacturerIdByName(string manufacturerName)
            => _dispoContext.Manufacturers.Where(x => x.Name == manufacturerName)
                                          .Select(s => s.Id)
                                          .FirstOrDefault()
                                          .ToLong();
    }
}