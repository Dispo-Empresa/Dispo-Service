﻿using Dispo.API.ResponseBuilder;
using Dispo.Domain.DTOs.Plugin;
using Dispo.Domain.DTOs.Request;
using Dispo.Domain.Exceptions;
using Dispo.Service.Services.Interfaces;
using Dispo.Service.Services.PasswordRecovery.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/forgot-password")]
    [ApiController]
    [AllowAnonymous]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordRecoveryService _passwordRecoveryService;

        public ForgotPasswordController(IAccountService accountService, IPasswordRecoveryService emailRecoveryService)
        {
            _accountService = accountService;
            _passwordRecoveryService = emailRecoveryService;
        }

        [HttpPost]
        [Route("send-recovery-token")]
        public IActionResult SendEmailCodeResetPassword([FromBody] string emailTo)
        {
            try
            {
                _passwordRecoveryService.SendRecoveryToken(emailTo);

                return Ok(new ResponseModelBuilder().WithMessage("O email será enviado em instantes.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (HttpRequestException)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"Serviço não encontrado ou fora do ar")
                                                            .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                            .Build());
            }
        }

        [HttpPost]
        [Route("validate-recovery-token")]
        public IActionResult VerifyEmailCode([FromBody] VerifyEmailCodeRequestDto verifyEmailCodeRequestDto)
        {
            try
            {
                _passwordRecoveryService.ValidateInputedToken(verifyEmailCodeRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Código validado.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (PasswordRecoveryException ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build());
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
        {
            try
            {
                _accountService.ResetPassword(resetPasswordRequestDto.AccountId, resetPasswordRequestDto.NewPassword);

                return Ok(new ResponseModelBuilder().WithMessage("Senha alterada.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}