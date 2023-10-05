using Dispo.Domain.Entities;
using Dispo.Infrastructure.Context;
using Dispo.Infrastructure.Repositories.Interfaces;

namespace Dispo.Infrastructure.Repositories
{
    public class BatchMovementRepository : BaseRepository<BatchMovement>, IBatchMovementRepository
    {
        private readonly DispoContext _dispoContext;

        public BatchMovementRepository(DispoContext dispoContext) : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }
    }
}
