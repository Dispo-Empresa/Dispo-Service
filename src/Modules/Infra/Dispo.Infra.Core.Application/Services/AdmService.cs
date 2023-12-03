using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Core.Application.Models.Request;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;

namespace Dispo.Infra.Core.Application.Services
{
    public class AdmService : IAdmService
    {
        public readonly IAccountRepository _accountRepository;
        public readonly IWarehouseAccountRepository _houseAccountRepository;

        public AdmService(IAccountRepository accountRepository, IWarehouseAccountRepository houseAccountRepository)
        {
            _accountRepository = accountRepository;
            _houseAccountRepository = houseAccountRepository;
        }

        public void CreateEmployee(CreateEmployeeRequestModel createEmployeeRequestDto)
        {
            try
            {
                var newAccount = new Account();
                newAccount.Email = createEmployeeRequestDto.Email;
                newAccount.Ativo = true;
                newAccount.Password = "alterarsenha";
                newAccount.RoleId = createEmployeeRequestDto.RoleId;

                _accountRepository.Create(newAccount);

                foreach (var warehouseId in createEmployeeRequestDto.WarehousesId)
                {
                    var newWarehouseAccount = new WarehouseAccount();
                    newWarehouseAccount.WarehouseId = warehouseId;
                    newWarehouseAccount.AccountId = newAccount.Id;

                    _houseAccountRepository.Create(newWarehouseAccount);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}