using Dispo.Shared.Core.Domain.DTOs.Request;
using Dispo.Shared.Core.Domain.DTOs.Response;

namespace Dispo.Account.Core.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Shared.Core.Domain.Entities.Account?> GetByIdAsyncFromCache(long id);

        Task<SignInResponseDto> AuthenticateByEmailAndPassword(string email, string password);

        UserResponseDto CreateAccountAndUser(SignUpRequestDto signUpModel);

        void ResetPassword(long accountId, string newPassword);

        UserAccountResponseDto UpdateUserAccountInfo(UserAccountResponseDto userAccountModel);

        void LinkWarehouses(List<long> warehouseIds, long userId);

        void ChangeWarehouse(long userId, long warehouseId);
    }
}