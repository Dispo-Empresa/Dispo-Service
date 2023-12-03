using Dispo.API.ResponseBuilder;
using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Product.Core.Application.Interfaces;
using Dispo.Product.Core.Application.Models;
using Dispo.Shared.Core.Domain;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/warehouses")]
    [ApiController]
    [Authorize(Roles = RolesManager.Manager)]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseService _warehouseService;
        private readonly IAccountResolverService _accountResolverService;

        public WarehouseController(IWarehouseRepository warehouseRepository, IWarehouseService warehouseService, IAccountResolverService accountResolverService)
        {
            _warehouseRepository = warehouseRepository;
            _warehouseService = warehouseService;
            _accountResolverService = accountResolverService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] WarehouseRequestModel warehouseRequestDto)
        {
            try
            {
                _warehouseService.CreateWarehouse(warehouseRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("O depósito foi criado com sucesso.")
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .WithSuccess(false)
                                                            .Build());
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var warehouses = _warehouseRepository.GetWarehouseInfo();

                return Ok(new ResponseModelBuilder().WithData(warehouses)
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("warehouses not found: " + ex.Message)
                                                            .Build());
            }
        }

        [HttpGet("get-with-address-by-user")]
        public IActionResult GetWarehouseWithAddressByUserId()
        {
            try
            {
                var accountId = _accountResolverService.GetLoggedAccountId() ?? throw new NotFoundException("Faça o login no sistema");
                var warehouses = _warehouseRepository.GetWarehousesWithAddressByAccountId(accountId);

                return Ok(new ResponseModelBuilder().WithData(warehouses)
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

        [HttpGet("get-with-address")]
        public IActionResult GetWarehouseWithAddress()
        {
            try
            {
                var warehouses = _warehouseRepository.GetWarehousesWithAddress();

                return Ok(new ResponseModelBuilder().WithData(warehouses)
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