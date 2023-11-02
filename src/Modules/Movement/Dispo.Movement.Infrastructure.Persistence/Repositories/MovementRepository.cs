using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Dispo.Shared.Infrastructure.Persistence.Repositories;

namespace Dispo.Movement.Infrastructure.Persistence.Repositories
{
    public class MovementRepository : BaseRepository<Shared.Core.Domain.Entities.Movement>, IMovementRepository
    {
        public MovementRepository(DispoContext dispoContext)
            : base(dispoContext)
        {
        }
    }
}