using Dispo.Product.Core.Application.Interfaces;
using Dispo.Product.Core.Application.Models;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using System.Transactions;

namespace Dispo.Product.Core.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public void CreateWarehouse(WarehouseRequestModel warehouseRequestDto)
        {
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var warehouse = new Warehouse
                {
                    Name = warehouseRequestDto.Name,
                    Country = warehouseRequestDto.Country,
                    UF = warehouseRequestDto.UF,
                    City = warehouseRequestDto.City,
                    District = warehouseRequestDto.District,
                    CEP = warehouseRequestDto.CEP,
                    AdditionalInfo = warehouseRequestDto.AdditionalInfo,
                };

                _warehouseRepository.Create(warehouse);
                tc.Complete();
            }
        }
    }
}