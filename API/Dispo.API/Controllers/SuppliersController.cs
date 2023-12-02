using Dispo.API.ResponseBuilder;
using Dispo.Product.Infrastructure.Persistence.Repositories;
using Dispo.PurchaseOrder.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain.DTOs.Request;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/suppliers")]
    [ApiController]
    [Authorize]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierRepository supplierRepository, ISupplierService supplierService)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SupplierRequestDto supplierRequestDto)
        {
            try
            {
                var supplierCreatedId = _supplierService.CreateSupplier(supplierRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Fornecedor criado com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithData(supplierCreatedId)
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

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit([FromBody] SupplierRequestDto supplierRequestDto)
        {
            try
            {
                _supplierService.UpdateSupplier(supplierRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Fornecedor atualizado com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
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
                var suppliers = _supplierRepository.GetSupplierInfoDto();

                return Ok(new ResponseModelBuilder().WithData(suppliers)
                                                    .WithSuccess(true)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Fornecedor não encontrado: " + ex.Message)
                                                            .Build());
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
               var supplier = _supplierRepository.GetById(id);

                if (supplier != null)
                {
                    var address = _supplierService.GetSupplierAddressById(supplier.AddressId);
                    supplier.Address = address;
                }

                return Ok(new ResponseModelBuilder().WithMessage("Busca pelo fornecedor realizada com sucesso")
                                                    .WithSuccess(true)
                                                    .WithData(supplier)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}