using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;

namespace Dispo.Infra.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly DispoContext _dispoContext;

        public RoleRepository(DispoContext dispoContext) : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }

        public List<RoleInfoDto> GetRoleInfo()
            => _dispoContext.Roles
                            .ToList()
                            .Select(s => new RoleInfoDto()
                            {
                                Id = s.Id,
                                Name = s.Name
                            })
                            .ToList();
    }
}