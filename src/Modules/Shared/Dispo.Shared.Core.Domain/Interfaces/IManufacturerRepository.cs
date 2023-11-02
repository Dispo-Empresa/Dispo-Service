using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IManufacturerRepository : IBaseRepository<Manufacturer>
    {
        IEnumerable<ManufacturerInfoDto> GetManufacturerInfoDto();

        long GetManufacturerIdByName(string manufacturerName);
    }
}