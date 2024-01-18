using Dispo.PurchaseOrder.Core.Application.Interfaces;
using Dispo.PurchaseOrder.Core.Application.Models;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Utils;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Dispo.PurchaseOrder.Core.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public long CreateSupplier(SupplierRequestModel supplierRequestDto)
        {
            if (_supplierRepository.GetSupplierIdByName(supplierRequestDto.Name).IsIdValid())
                throw new AlreadyExistsException("Já existe o fornecedor informado");

            long supplierCreatedId = IDHelper.INVALID_ID;

            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var supplier = new Supplier()
                {
                    Name = supplierRequestDto.Name,
                    ContactName = supplierRequestDto.ContactName,
                    ContactTitle = supplierRequestDto.ContactTitle,
                    Cnpj = Regex.Replace(supplierRequestDto.Cnpj, @"[^\d]", ""),
                    Email = supplierRequestDto.Email,
                    Phone = Regex.Replace(supplierRequestDto.Phone, @"[^\d]", ""),
                };

                _supplierRepository.Create(supplier);
                tc.Complete();
                supplierCreatedId = supplier.Id;
            }

            return supplierCreatedId;
        }

        public void UpdateSupplier(SupplierRequestModel supplierRequestDto)
        {
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var supplier = new Supplier()
                {
                    Id = supplierRequestDto.Id,
                    Name = supplierRequestDto.Name,
                    ContactName = supplierRequestDto.ContactName,
                    ContactTitle = supplierRequestDto.ContactTitle,
                    Cnpj = supplierRequestDto.Cnpj,
                    Email = supplierRequestDto.Email,
                    Phone = supplierRequestDto.Phone
                };

                _supplierRepository.Update(supplier);
                tc.Complete();
            }
        }


    }
}