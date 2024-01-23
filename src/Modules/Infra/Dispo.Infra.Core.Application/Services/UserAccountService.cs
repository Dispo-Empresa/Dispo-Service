using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Core.Application.Models.Response;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using System.Transactions;

namespace Dispo.Infra.Core.Application.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public UserAccountService(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public UserAccountResponseModel UpdateUserAccountInfo(long id, UserAccountResponseModel userAccountModel)
        {
            User? userInfo = null;
            Account? accountInfo = null;

            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var accountUpdated = _accountRepository.GetById(id);

                if (accountUpdated is null)
                    throw new Exception("Conta não encontrada");

                accountUpdated.Email = userAccountModel.Email;

                accountInfo = _accountRepository.Update(accountUpdated);

                var userUpdated = _userRepository.GetByExpression(w => w.Id == accountUpdated.UserId).FirstOrDefault();
                if (userUpdated is null)
                    throw new Exception("Informações não encontradas para esta conta!");

                userUpdated.FirstName = userAccountModel.FirstName;
                userUpdated.LastName = userAccountModel.LastName;
                userUpdated.Phone = userAccountModel.Phone;

                userInfo = _userRepository.Update(userUpdated);

                tc.Complete();
            }

            return new UserAccountResponseModel()
            {
                Id = id,
                Email = accountInfo.Email,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Phone = userInfo.Phone,
            };
        }
    }
}