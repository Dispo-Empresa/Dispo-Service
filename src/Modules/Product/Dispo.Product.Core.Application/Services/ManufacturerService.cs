using Dispo.Product.Core.Application.Interfaces;
using Dispo.Product.Core.Application.Models;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Utils;
using Dispo.Shared.Utils.Extensions;
using System.Transactions;

namespace Dispo.Product.Core.Application.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public long CreateManufacturer(ManufacturerRequestModel manufacturerRequestDto)
        {
            if (_manufacturerRepository.GetManufacturerIdByName(manufacturerRequestDto.Name).IsIdValid())
                throw new AlreadyExistsException("Já existe o fabricante informado");

            long manufacturerCreatedId = IDHelper.INVALID_ID;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var manufacturer = new Manufacturer()
                {
                    Name = manufacturerRequestDto.Name,
                    Logo = manufacturerRequestDto.Logo.ConvertToByteArray()
                };

                var manufacturerCreated = _manufacturerRepository.Create(manufacturer);
                tc.Complete();

                manufacturerCreatedId = manufacturer.Id;
            }

            return manufacturerCreatedId;
        }

        public void UpdateManufacturer(ManufacturerRequestModel manufacturerRequestDto)
        {
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var manufacturer = new Shared.Core.Domain.Entities.Manufacturer()
                {
                    Id = manufacturerRequestDto.Id,
                    Name = manufacturerRequestDto.Name,
                    Logo = manufacturerRequestDto.Logo.ConvertToByteArray()
                };

                _manufacturerRepository.Update(manufacturer);
                tc.Complete();
            }
            
        }
    }
}