using Dispo.API.ResponseBuilder;
using Dispo.Domain;
using Dispo.Domain.DTOs;
using Dispo.Domain.DTOs.RequestDTOs;
using Dispo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.APIs.Controllers
{
    [Route("/api/v1/movements")]
    [ApiController]
    [Authorize(Roles = Roles.WarehouseOperator)]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementService _movementService;

        public MovementsController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        /// <summary>
        /// Realiza a movimentação de um produto.
        /// </summary>
        [HttpPost]
        [Route("move")]
        public async Task<IActionResult> MoveProduct([FromBody] ProductMovimentationDto productMovimentationDto)
        {
            try
            {
                productMovimentationDto.Validate();
                await _movementService.MoveProductAsync(productMovimentationDto);

                return Ok(new ResponseModelBuilder().WithMessage("Movimentação de produto realizada com sucesso.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build());
            }
        }

        /// <summary>
        /// Realiza a movimentação de entrada em lote.
        /// </summary>
        [HttpPost]
        [Route("move/batch")]
        [AllowAnonymous]
        public async Task<IActionResult> BatchInputMovement([FromBody] BatchMovimentationDto batchMovimentationDto)
        {
            try
            {
                await _movementService.MoveBatchAsync(batchMovimentationDto);
                return Ok(new ResponseModelBuilder().WithMessage("Movimentação de produto realizada com sucesso.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build());
            }
        }
    }
}