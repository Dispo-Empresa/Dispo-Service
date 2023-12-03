using Dispo.Infra.Core.Application.Models.Response;

namespace Dispo.Infra.Core.Application.Interfaces
{
    public interface IUserAccountService
    {
        UserAccountResponseModel UpdateUserAccountInfo(long id, UserAccountResponseModel userAccountModel);
    }
}