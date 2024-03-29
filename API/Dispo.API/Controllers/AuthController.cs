﻿using Dispo.API.ResponseBuilder;
using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Core.Application.Models.Request;
using Dispo.Shared.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IAccountService accountService, ITokenGenerator tokenGenerator)
        {
            _accountService = accountService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel signInRequestDto)
        {
            try
            {
                var infoAccount = await _accountService.AuthenticateByEmailAndPassword(signInRequestDto.Email, signInRequestDto.Password);
                var generatedToken = _tokenGenerator.GenerateJwtToken(infoAccount.AccountId, infoAccount.CurrentWarehouseId);

                return Ok(new ResponseModelBuilder().WithMessage("User exists!")
                                                    .WithSuccess(true)
                                                    .WithData(generatedToken)
                                                    .Build());
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ResponseModelBuilder().WithMessage(ex.Message)
                                                          .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                            .Build());
            }
        }
    }
}