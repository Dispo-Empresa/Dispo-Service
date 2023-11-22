using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Entities.Account>
    {
        bool ExistsByEmail(string email);

        bool ExistsByEmailAndPassword(string email, string password);

        Entities.Account? GetAccountByEmailAndPassword(string email, string password);

        void ResetPassword(Entities.Account account, string newPassword);

        long GetAccountIdByEmail(string email);

        UserInfoResponseDto GetUserInfoResponseDto(long id);

        string GetUserNameByAccountId(long id);

        string GetRoleKeyByAccountId(long id);

        IList<AccountUserInfoDto> GetAccountsUserInfo();

        Entities.Account? GetWithWarehousesById(long id);

        DateTime GetLastLicenceCheckCurrentByCompanyId(long companyId);

        void UpdateLastLicenceCheckById(long accountId);
    }
}