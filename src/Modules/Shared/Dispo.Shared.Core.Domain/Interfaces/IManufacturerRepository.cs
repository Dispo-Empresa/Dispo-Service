using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IManufacturerRepository : IBaseRepository<Manufacturer>
    {
        IEnumerable<ManufacturerInfoDto> GetManufacturerInfoDto();

        long GetManufacturerIdByName(string manufacturerName);
    }
}