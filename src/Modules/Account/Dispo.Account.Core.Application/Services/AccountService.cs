using AutoMapper;
using Dispo.Account.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain.DTOs.Request;
using Dispo.Shared.Core.Domain.DTOs.Response;
using Dispo.Shared.Core.Domain.Endpoints;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Core.Domain.Models;
using Dispo.Shared.Utils;
using EscNet.Cryptography.Interfaces;
using EscNet.Hashers.Interfaces.Algorithms;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Transactions;

namespace Dispo.Account.Core.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IArgon2IdHasher _hasher;
        private readonly IRijndaelCryptography _rijndaelCryptography;
        private readonly IMapper _mapper;
        private readonly IWarehouseRepository _warehouseRepository;

        public AccountService(IMemoryCache memoryCache, IAccountRepository accountRepository, IUserRepository userRepository, IArgon2IdHasher hasher, IRijndaelCryptography rijndaelCryptography, IMapper mapper, IWarehouseRepository warehouseRepository)
        {
            _memoryCache = memoryCache;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _hasher = hasher;
            _rijndaelCryptography = rijndaelCryptography;
            _mapper = mapper;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<Shared.Core.Domain.Entities.Account?> GetByIdAsyncFromCache(long id)
        {
            return await _memoryCache.GetOrCreateAsync(id, async entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(10);
                return await _accountRepository.GetByIdAsync(id);
            });
        }

        public async Task<SignInResponseDto> AuthenticateByEmailAndPassword(string email, string password)
        {
            var encryptedEmail = email;
            var hashedPassword = password;

            var loggedAccount = _accountRepository.GetAccountByEmailAndPassword(encryptedEmail, hashedPassword);

            if (loggedAccount == null)
                throw new NotFoundException("Conta não encontrada");

            await ValidateLicence(loggedAccount.Id, loggedAccount.CompanyIdByHub);

            return new SignInResponseDto()
            {
                AccountId = loggedAccount.Id,
                CurrentWarehouseId = loggedAccount.CurrentWarehouseId ?? _warehouseRepository.GetAllAsNoTracking().First().Id,
            };
        }

        public UserResponseDto CreateAccountAndUser(SignUpRequestDto signUpRequestDto)
        {
            if (_accountRepository.ExistsByEmail(signUpRequestDto.Email))
                throw new AlreadyExistsException("Já existe um usuário com o Email informado");

            if (_userRepository.ExistsByCpfCnpj(signUpRequestDto.CpfCnpj))
                throw new AlreadyExistsException("Já existe um usuário com o CPF/CNPJ informado");

            UserResponseDto userDto;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = new User
                {
                    BirthDate = signUpRequestDto.BirthDate,
                    Cpf = signUpRequestDto.CpfCnpj,
                    FirstName = signUpRequestDto.FirstName,
                    LastName = signUpRequestDto.LastName,
                    Phone = signUpRequestDto.Phone,
                    Account = new Shared.Core.Domain.Entities.Account
                    {
                        Email = _rijndaelCryptography.Encrypt(signUpRequestDto.Email),
                        Password = _hasher.Hash(signUpRequestDto.Password),
                    },
                };

                var createdUser = _userRepository.Create(user);
                tc.Complete();

                userDto = _mapper.Map<UserResponseDto>(createdUser);
            }
            return userDto;
        }

        public void ResetPassword(long accountId, string newPassword)
        {
            var account = _accountRepository.GetById(accountId);

            if (account != null)
            {
                using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _accountRepository.ResetPassword(account, _hasher.Hash(newPassword));
                    tc.Complete();
                }
            }
        }

        public UserAccountResponseDto UpdateUserAccountInfo(UserAccountResponseDto userAccountModel)
        {
            Shared.Core.Domain.Entities.User? userInfo = null;

            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var userUpdated = new User();

                if (userUpdated == null)
                    throw new Exception("Informações não encontradas para esta conta!");

                userUpdated.BirthDate = userAccountModel.BirthDate;
                userUpdated.Cpf = userAccountModel.CpfCnpj;
                userUpdated.FirstName = userAccountModel.FirstName;
                userUpdated.LastName = userAccountModel.LastName;
                userUpdated.Phone = userAccountModel.Phone;

                userInfo = _userRepository.Update(userUpdated);

                tc.Complete();
            }

            return new UserAccountResponseDto()
            {
                Id = userInfo.Id.ToLong(),
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                BirthDate = userInfo.BirthDate,
                Phone = userInfo.Phone,
                CpfCnpj = userInfo.Cpf,
            };
        }

        public void LinkWarehouses(List<long> warehouseIds, long userId)
        {
            var user = _accountRepository.GetWithWarehousesById(userId) ?? throw new NotFoundException("Esse usuário não existe.");
            foreach (var warehouseId in warehouseIds)
            {
                if (user.WarehouseAccounts.Any(w => w.WarehouseId == warehouseId && w.AccountId == userId))
                    continue;

                user.WarehouseAccounts.Add(new WarehouseAccount(warehouseId, userId));
            }

            _accountRepository.Update(user);
        }

        public void ChangeWarehouse(long userId, long warehouseId)
        {
            var user = _accountRepository.GetById(userId) ?? throw new NotFoundException("Esse usuário não existe.");
            user.CurrentWarehouseId = warehouseId;
            _accountRepository.Update(user);
        }

        public async Task ValidateLicence(long accountId, long companyId)
        {
            var lastLicenceCheck = _accountRepository.GetLastLicenceCheckCurrentByCompanyId(companyId);

            if (lastLicenceCheck.AddDays(1) >= DateTime.Now)
                return;

            await ValidateLicenceByHub(accountId, companyId);
        }

        public async Task ValidateLicenceByHub(long accountId, long companyId)
        {
            try
            {
                var url = HubEndpoints.GetLicence;
                var parameters = $"?companyId={companyId}";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(parameters);

                var jsonString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<ResponseModel>(jsonString);

                if (!responseModel.Success)
                    throw new BusinessException($"Falha ao obter a licença: {responseModel.Message}");

                _accountRepository.UpdateLastLicenceCheckById(accountId);
            }
            catch (BusinessException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na comunicação com o DispoHub: {ex.Message}");
            }
        }
    }
}