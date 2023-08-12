using Dispo.API.ResponseBuilder;
using Dispo.APIs.Models;
using Dispo.Domain.Dtos;
using Dispo.Domain.Entities;
using Dispo.Domain.Exceptions;
using Dispo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.APIs.Controllers
{
    [Route("/api/v1/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService warehouseService;
        private readonly IUserResolverService userResolverService;

        public WarehouseController(IWarehouseService warehouseService, IUserResolverService userResolverService)
        {
            this.warehouseService = warehouseService;
            this.userResolverService = userResolverService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateWarehouseModel model)
        {
            try
            {
                warehouseService.Create(new Warehouse
                {
                    AdressId = model.AddressId,
                    Name = model.Name,
                    CompanyId = 1,
                });

                return Ok(new ResponseModelBuilder().WithMessage("O depósito foi criado com sucesso.")
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

        [HttpGet("get-with-address-by-user")]
        public IActionResult GetWarehouseWithAddressByUserId()
        {
            try
            {
                var userId = userResolverService.GetLoggedUserId() ?? throw new NotFoundException("Faça o login no sistema.");
                var warehouses = warehouseService.GetWarehousesWithAddressByUserId(userId);

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
                var warehouses = warehouseService.GetWarehousesWithAddress();

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
