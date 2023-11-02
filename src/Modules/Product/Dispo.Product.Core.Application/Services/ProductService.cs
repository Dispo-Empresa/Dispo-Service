using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Product.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.DTOs.Request;
using System.Transactions;
using AutoMapper;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Utils;

namespace Dispo.Product.Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        #region Public Methods

        public long CreateProduct(ProductRequestDto productModel)
        {
            if (_productRepository.GetProductIdByName(productModel.Name).IsIdValid())
                throw new AlreadyExistsException("Já existe o produto informado");

            long productCreatedId = IDHelper.INVALID_ID;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var product = new Shared.Core.Domain.Entities.Product()
                {
                    Name = productModel.Name,
                    UnitOfMeasurement = productModel.UnitOfMeasurement,
                    SalePrice = 0,
                    Category = productModel.Category,
                    Description = productModel.Description
                };

                var productCreated = _productRepository.Create(product);
                tc.Complete();

                productCreatedId = product.Id;
            }

            return productCreatedId;
        }

        public string BuildProductSKUCode(string productName, string productType)
        {
            var productNameWords = productName.Split(' ').ToList();
            var productSKUCode = "";

            productNameWords.ForEach(x => productSKUCode += x.Count() > 2 && productName.Count() < 25 ? x.Substring(0, 3) : x.Substring(0, 2));
            productSKUCode += productType.Substring(0, 3);
            productSKUCode += DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString().Count();

            productSKUCode = productSKUCode.ToUpper();

            //if (_productRepository.GetProductIdByCode(productSKUCode).IsIdValid())
            //{
            //    // Ja existe o código
            //}

            return productSKUCode;
        }

        public async Task<bool> ExistsByIdAsync(long productId)
        {
            return await _productRepository.ExistsByIdAsync(productId);
        }

        public List<ProductInfoDto> GetWithActivePurschaseOrder()
        {
            return _productRepository.GetWithActivePurschaseOrder();
        }

        #endregion Public Methods
    }
}