using Dispo.Service.DTOs.RequestDTOs;
using Dispo.Service.DTOs.ResponseDTOs;

namespace Dispo.Service.Services.Interfaces
{
    public interface IAccountService
    {
        UserAccountResponseDto GetUserWithAccountByEmailAndPassword(string email, string password);

        UserResponseDto CreateAccountAndUser(SignUpRequestDto signUpModel);

        void ResetPassword(long accountId, string newPassword);

        UserAccountResponseDto UpdateUserAccountInfo(UserAccountResponseDto userAccountModel);
    }
}