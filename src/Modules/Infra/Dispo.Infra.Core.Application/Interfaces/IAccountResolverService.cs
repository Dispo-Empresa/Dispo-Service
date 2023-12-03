using System.Security.Claims;

namespace Dispo.Infra.Core.Application.Interfaces
{
    public interface IAccountResolverService
    {
        long? GetLoggedAccountId();

        long? GetLoggedWarehouseId();

        IEnumerable<Claim?> GetLoggedAccountDynamicClaims(params string[] claims);
    }
}