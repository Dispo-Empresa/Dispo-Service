using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IRoleRepository
    {
        List<RoleInfoDto> GetRoleInfo();
    }
}