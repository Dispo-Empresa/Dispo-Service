﻿using Dispo.API.ResponseBuilder;
using Dispo.Product.Core.Application.Interfaces;
using Dispo.Product.Core.Application.Models;
using Dispo.Shared.Core.Domain;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dispo.API.Controllers
{
    [Route("/api/v1/products")]
    [ApiController]
    [Authorize(Roles = RolesManager.AllRoles)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductService productService, IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductRequestModel productRequestDto)
        {
            try
            {
                var productCreatedId = _productService.CreateProduct(productRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Produto criado com sucesso!")
                                                    .WithSuccess(true)
                                                    .WithData(productCreatedId)
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

        [HttpPost("edit")]
        public IActionResult Edit([FromForm] ProductRequestModel productRequestDto)
        {
            try
            {
                _productService.UpdateProduct(productRequestDto);

                return Ok(new ResponseModelBuilder().WithMessage("Produto atualizado com sucesso!")
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

        [HttpGet("get-names")]
        public IActionResult GetProductNamesWithCode()
        {
            try
            {
                var productNames = _productRepository.GetAllProductNames();

                return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                    .WithData(productNames)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Products not found: " + ex.Message)
                                                            .Build());
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productRepository.GetProductInfoDto();

                return Ok(new ResponseModelBuilder().WithMessage("Movimentação de produto realizada com sucesso.")
                                                    .WithSuccess(true)
                                                    .WithData(products)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Products not found: " + ex.Message)
                                                            .Build());
            }
        }

        [HttpGet("{id}")]
        public  IActionResult Get(long id)
        {
            try
            {
                var product = _productRepository.GetById(id);

                return Ok(new ResponseModelBuilder().WithMessage("Busca pelo produto realizada com sucesso")
                                                    .WithSuccess(true)
                                                    .WithData(product)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("getPurchaseOrders")]
        public IActionResult GetPurchaseOrders()
        {
            try
            {
                var purchaseOrderInfo = _productRepository.GetPurchaseOrderInfoDto();

                return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                    .WithData(purchaseOrderInfo)
                                                    .WithAlert(AlertType.Success)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Products not found: " + ex.Message)
                                                            .Build());
            }
        }

        [HttpGet("get-with-active-pursche-orders/{movementType}")]
        public IActionResult GetWithActivePurschaseOrder([FromRoute] int movementType)
        {
            var products = _productRepository.GetWithActivePurschaseOrderByMovementType(movementType);
            return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                .WithData(products)
                                                .Build());
        }

        [HttpGet("get-with-saleprice")]
        public IActionResult GetWithSalePrice()
        {
            var products = _productRepository.GetWithSalePrice();
            return Ok(new ResponseModelBuilder().WithSuccess(true)
                                                .WithData(products)
                                                .Build());
        }
    }
}