using Dispo.Infra.Core.Application.Models.Response;

namespace Dispo.Infra.Core.Application.Interfaces
{
    public interface ITokenGenerator
    {
        TokenInfoDto GenerateJwtToken(long id, long currentWarehouseId);
    }
}