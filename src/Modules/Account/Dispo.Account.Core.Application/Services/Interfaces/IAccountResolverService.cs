using System.Security.Claims;

namespace Dispo.Account.Core.Application.Services.Interfaces
{
    public interface IAccountResolverService
    {
        long? GetLoggedAccountId();

        long? GetLoggedWarehouseId();

        IEnumerable<Claim?> GetLoggedAccountDynamicClaims(params string[] claims);
    }
}