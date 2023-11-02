using Dispo.Shared.Core.Domain.DTOs.Response;

namespace Dispo.Account.Core.Application.Services.Interfaces
{
    public interface IUserAccountService
    {
        UserAccountResponseDto UpdateUserAccountInfo(long id, UserAccountResponseDto userAccountModel);
    }
}