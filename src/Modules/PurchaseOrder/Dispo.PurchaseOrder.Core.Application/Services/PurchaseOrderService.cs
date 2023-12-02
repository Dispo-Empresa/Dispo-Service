using Dispo.Account.Core.Application.Services.Interfaces;
using Dispo.PurchaseOrder.Core.Application.Services.Interfaces;
using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.DTOs.Request;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Enums;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Utils;
using System.Transactions;

namespace Dispo.PurchaseOrder.Core.Application.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _PurchaseOrderRepository;
        private readonly IOrderRepository _OrderRepository;
        private readonly IAccountResolverService _AccountResolverService;
        private readonly ISupplierRepository _SupplierRepository;

        public PurchaseOrderService(IPurchaseOrderRepository PurchaseOrderRepository, IOrderRepository OrderRepository, IAccountResolverService AccountResolverService, ISupplierRepository supplierRepository)
        {
            _PurchaseOrderRepository = PurchaseOrderRepository;
            _OrderRepository = OrderRepository;
            _AccountResolverService = AccountResolverService;
            _SupplierRepository = supplierRepository;
        }

        public long CreatePurchaseOrder(PurchaseOrderRequestDto PurchaseOrderRequestDto)
        {
            try
            {
                long purchaseOrderCreatedId = IDHelper.INVALID_ID;

                using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var currentWareHouse = _AccountResolverService.GetLoggedWarehouseId();

                    var purchaseOrder = new Shared.Core.Domain.Entities.PurchaseOrder()
                    {
                        Number = PurchaseOrderRequestDto.Number,
                        CreationDate = PurchaseOrderRequestDto.CreationDate,
                        PaymentMethod = PurchaseOrderRequestDto.PaymentMethod,
                        NotificationType = PurchaseOrderRequestDto.NotificationType,
                        SupplierId = PurchaseOrderRequestDto.SupplierId,
                        WarehouseId = currentWareHouse.Value,
                    };
                    purchaseOrder.ChangeStatusPurchaseOrder(ePurchaseOrderActions.CREATING);

                    _PurchaseOrderRepository.Create(purchaseOrder);

                    purchaseOrderCreatedId = purchaseOrder.Id;

                    foreach (var orderDto in PurchaseOrderRequestDto.Orders)
                    {
                        var order = new Order()
                        {
                            Description = orderDto.Description,
                            Quantity = orderDto.Quantity,
                            TotalPrice = orderDto.TotalPrice,
                            ProductId = orderDto.Product,
                            PurchaseOrderId = purchaseOrderCreatedId
                        };

                        _OrderRepository.Create(order);
                    }

                    tc.Complete();
                }

                return purchaseOrderCreatedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Shared.Core.Domain.Entities.PurchaseOrder> FillPurchaseOrderWithSupplier(IEnumerable<Shared.Core.Domain.Entities.PurchaseOrder> purchaseOrderList)
        {
            foreach (var purchaseOrder in purchaseOrderList)
                purchaseOrder.Supplier = _SupplierRepository.GetById(purchaseOrder.SupplierId);

            return purchaseOrderList;
        }

        public List<PurschaseOrderDto> GetByProcuctId(long productId)
        {
            return _PurchaseOrderRepository.GetByProcuctId(productId);
        }
    }
}