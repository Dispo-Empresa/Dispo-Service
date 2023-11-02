using Dispo.Shared.Core.Domain.DTOs.Response;

namespace Dispo.Account.Core.Application.Services.Interfaces
{
    public interface ITokenGenerator
    {
        TokenInfoDto GenerateJwtToken(long id, long currentWarehouseId);
    }
}