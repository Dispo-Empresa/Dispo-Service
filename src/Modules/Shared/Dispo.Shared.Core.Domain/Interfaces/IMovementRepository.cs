using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IMovementRepository : IBaseRepository<Movement>
    {
        List<MovimentationDetailsDto> GetDetails();
    }
}