using Dispo.API.ResponseBuilder;
using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Core.Application.Models.Request;
using Dispo.Shared.Core.Domain;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("api/v1/adm")]
    [ApiController]
    [Authorize(Roles = RolesManager.Manager)]
    public class AdmController : ControllerBase
    {
        public readonly IAdmService _admService;
        public readonly IRoleRepository _roleRepository;
        private readonly IAccountRepository _accountRepository;

        public AdmController(IAdmService admService, IRoleRepository roleRepository, IAccountRepository accountRepository)
        {
            _admService = admService;
            _roleRepository = roleRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet("getRoles")]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _roleRepository.GetRoleInfo();

                return Ok(new ResponseModelBuilder().WithData(roles)
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
        }

        [HttpPost("createEmployee")]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeRequestModel createEmployeeRequestDto)
        {
            try
            {
                _admService.CreateEmployee(createEmployeeRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Funcionário criado com sucesso.")
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
        }

        [HttpGet("employees")]
        public IActionResult GetEmployees()
        {
            try
            {
                var employeesAccountInfo = _accountRepository.GetAccountsUserInfo();

                return Ok(new ResponseModelBuilder().WithData(employeesAccountInfo)
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
        }
    }
}