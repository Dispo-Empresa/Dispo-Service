using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        bool ExistsByEmail(string email);

        bool ExistsByEmailAndPassword(string email, string password);

        Account? GetAccountByEmailAndPassword(string email, string password);

        void ResetPassword(Account account, string newPassword);

        long GetAccountIdByEmail(string email);

        UserInfoResponseDto GetUserInfoResponseDto(long id);

        string GetUserNameByAccountId(long id);

        string GetRoleKeyByAccountId(long id);

        IList<AccountUserInfoDto> GetAccountsUserInfo();

        Account? GetWithWarehousesById(long id);
    }
}