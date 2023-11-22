using Dispo.API.ResponseBuilder;
using Dispo.Product.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain.DTOs.Request;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/manufacturers")]
    [ApiController]
    [Authorize]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IManufacturerService _manufacturerService;

        public ManufacturersController(IManufacturerRepository manufacturerRepository, IManufacturerService manufacturerService)
        {
            _manufacturerRepository = manufacturerRepository;
            _manufacturerService = manufacturerService;
        }

        [HttpPost]
        public IActionResult Create([FromForm] ManufacturerRequestDto manufacturerRequestDto)
        {
            try
            {
                var manufacturerCreatedId = _manufacturerService.CreateManufacturer(manufacturerRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Fabricante criado com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithData(manufacturerCreatedId)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"Erro inesperado:  {ex.Message}")
                                                            .WithSuccess(false)
                                                            .WithAlert(AlertType.Error)
                                                            .Build());
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var manufacturers = _manufacturerRepository.GetManufacturerInfoDto();

                return Ok(new ResponseModelBuilder().WithData(manufacturers)
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Manufacturers not found: " + ex.Message)
                                                            .Build());
            }
        }
    }
}