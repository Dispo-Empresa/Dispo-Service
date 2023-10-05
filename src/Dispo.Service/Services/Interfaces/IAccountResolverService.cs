using Dispo.Domain.Entities;
using System.Security.Claims;

namespace Dispo.Service.Services.Interfaces
{
    public interface IAccountResolverService
    {
        long? GetLoggedAccountId();
        long? GetLoggedWarehouseId();
        IEnumerable<Claim?> GetLoggedAccountDynamicClaims(params string[] claims);
    }
}