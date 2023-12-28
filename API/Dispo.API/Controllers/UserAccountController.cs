using Dispo.API.ResponseBuilder;
using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Core.Application.Models.Response;
using Dispo.Shared.Core.Domain;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/user-account")]
    [ApiController]
    [Authorize(Roles = RolesManager.AllRoles)]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IAccountRepository _accountRepository;

        public UserAccountController(IUserAccountService userAccountService, IAccountRepository accountRepository)
        {
            _userAccountService = userAccountService;
            _accountRepository = accountRepository;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserAccountInfo([FromRoute] long accountId, [FromBody] UserAccountResponseModel userAccountModel)
        {
            try
            {
                var response = _userAccountService.UpdateUserAccountInfo(accountId, userAccountModel);

                return Ok(new ResponseModelBuilder().WithMessage("Dados atualizados com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithData(response)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"Erro não esperado: {ex.Message}")
                                                            .WithSuccess(false)
                                                            .WithData(ex)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
        }

        [HttpGet("id={id}")]
        public IActionResult GetAllUserInfo([FromRoute] long id)
        {
            try
            {
                var userAccountInfo = _accountRepository.GetUserInfoResponseDto(id);

                return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                    .WithData(userAccountInfo)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"Erro não esperado: {ex.Message}")
                                                            .WithSuccess(false)
                                                            .WithData(ex)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
        }

        [HttpPost("getAccountIdByEmail")]
        [AllowAnonymous]
        public IActionResult GetAccountIdByEmail([FromBody] string email)
        {
            try
            {
                var accountId = _accountRepository.GetAccountIdByEmail(email);

                return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                    .WithData(accountId)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"Erro não esperado: {ex.Message}")
                                                            .WithSuccess(false)
                                                            .Build());
            }
        }
    }
}