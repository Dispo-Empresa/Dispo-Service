using Dispo.API.ResponseBuilder;
using Dispo.APIs.Models;
using Dispo.APIs.ResponseBuilder;
using Dispo.Domain.Exceptions;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.DTOs.ResponseDTOs;
using Dispo.Service.Services;
using Dispo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAccountService userAccountService;
        private readonly IAccountRepository accountRepository;
        private readonly IUserService userService;
        private readonly IUserResolverService userResolverService;

        public UserController(IUserAccountService userAccountService, IAccountRepository accountRepository, IUserService userService, IUserResolverService userResolverService)
        {
            this.userAccountService = userAccountService;
            this.accountRepository = accountRepository;
            this.userService = userService;
            this.userResolverService = userResolverService;
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateUserAccountInfo(long accountId, [FromBody] UserAccountResponseDto userAccountModel)
        {
            try
            {
                var response = userAccountService.UpdateUserAccountInfo(accountId, userAccountModel);

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

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult GetAllUserInfo(long id)
        {
            try
            {
                var userAccountInfo = accountRepository.GetUserInfoResponseDto(id);

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

        [HttpPost]
        [AllowAnonymous]
        [Route("link-warehouses")]
        public IActionResult LinkWarehouses([FromBody] LinkWarehousesModel model)
        {
            try
            {
                userService.LinkWarehouses(model.Warehouses, model.UserId);

                return Ok(new ResponseModelBuilder().WithMessage("O depósito foi vinculado ao usuário.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build()); ;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("change-warehouse")]
        public IActionResult ChangeWarehouse([FromBody] long warehouseId)
        {
            try
            {
                var userId = userResolverService.GetLoggedUserId() ?? throw new NotFoundException("Faça o login no sistema.");
                userService.ChangeWarehouse(userId, warehouseId);

                return Ok(new ResponseModelBuilder().WithMessage("O depósito foi vinculado ao usuário.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build()); ;
            }
        }
    }
}