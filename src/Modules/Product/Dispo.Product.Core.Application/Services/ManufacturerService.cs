using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Product.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain.DTOs.Request;
using System.Transactions;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Common.Utils;

namespace Dispo.Product.Core.Application.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public long CreateManufacturer(ManufacturerRequestDto manufacturerRequestDto)
        {
            if (_manufacturerRepository.GetManufacturerIdByName(manufacturerRequestDto.Name).IsIdValid())
                throw new AlreadyExistsException("Já existe o fabricante informado");

            long manufacturerCreatedId = IDHelper.INVALID_ID;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var manufacturer = new Manufacturer()
                {
                    Name = manufacturerRequestDto.Name,
                };

                var manufacturerCreated = _manufacturerRepository.Create(manufacturer);
                tc.Complete();

                manufacturerCreatedId = manufacturer.Id;
            }

            return manufacturerCreatedId;
        }
    }
}