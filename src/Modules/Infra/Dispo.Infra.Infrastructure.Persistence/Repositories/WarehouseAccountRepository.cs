using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;

namespace Dispo.Infra.Infrastructure.Persistence.Repositories
{
    public class WarehouseAccountRepository : BaseRepository<WarehouseAccount>, IWarehouseAccountRepository
    {
        public WarehouseAccountRepository(DispoContext dispoContext) : base(dispoContext)
        {
        }
    }
}