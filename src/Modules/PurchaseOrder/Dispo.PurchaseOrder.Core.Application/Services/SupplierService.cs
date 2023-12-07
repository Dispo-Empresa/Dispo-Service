﻿using Dispo.PurchaseOrder.Core.Application.Interfaces;
using Dispo.PurchaseOrder.Core.Application.Models;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Utils;
using System.Transactions;

namespace Dispo.PurchaseOrder.Core.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository, IAddressRepository addressRepository)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public long CreateSupplier(SupplierRequestModel supplierRequestDto)
        {
            if (_supplierRepository.GetSupplierIdByName(supplierRequestDto.Name).IsIdValid())
                throw new AlreadyExistsException("Já existe o fornecedor informado");

            long supplierCreatedId = IDHelper.INVALID_ID;
            long addressCreatedId = IDHelper.INVALID_ID;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var address = new Address()
                {
                    Country = supplierRequestDto.Address.Country,
                    UF = supplierRequestDto.Address.UF,
                    City = supplierRequestDto.Address.City,
                    District = supplierRequestDto.Address.District,
                    CEP = supplierRequestDto.Address.CEP,
                    AdditionalInfo = supplierRequestDto.Address.AdditionalInfo ?? string.Empty,
                };

                _addressRepository.Create(address);
                tc.Complete();
                addressCreatedId = address.Id;
            }

            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var supplier = new Supplier()
                {
                    Name = supplierRequestDto.Name,
                    ContactName = supplierRequestDto.ContactName,
                    ContactTitle = supplierRequestDto.ContactTitle,
                    Cnpj = supplierRequestDto.Cnpj,
                    Email = supplierRequestDto.Email,
                    Phone = supplierRequestDto.Phone,
                    AddressId = addressCreatedId
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
                var supplier = new Shared.Core.Domain.Entities.Supplier()
                {
                    Id = supplierRequestDto.Id,
                    AddressId = supplierRequestDto.Address.AddressId,
                    Name = supplierRequestDto.Name,
                    ContactName = supplierRequestDto.ContactName,
                    ContactTitle = supplierRequestDto.ContactTitle,
                    Cnpj = supplierRequestDto.Cnpj,
                    Email = supplierRequestDto.Email,
                    Phone = supplierRequestDto.Phone
                };

                var address = _addressRepository.GetById(supplierRequestDto.Address.AddressId);

                supplier.Address = address;

                _supplierRepository.Update(supplier);
                tc.Complete();
            }
        }


    }
}