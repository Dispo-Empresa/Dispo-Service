using Dispo.Infra.Core.Application.Models.Request;
using Dispo.Infra.Core.Application.Models.Response;

namespace Dispo.Infra.Core.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Shared.Core.Domain.Entities.Account?> GetByIdAsyncFromCache(long id);

        Task<SignInResponseModel> AuthenticateByEmailAndPassword(string email, string password);

        UserResponseModel CreateAccountAndUser(SignUpRequestModel signUpModel);

        void ResetPassword(long accountId, string newPassword);

        UserAccountResponseModel UpdateUserAccountInfo(UserAccountResponseModel userAccountModel);

        void LinkWarehouses(List<long> warehouseIds, long userId);

        void ChangeWarehouse(long userId, long warehouseId);
    }
}