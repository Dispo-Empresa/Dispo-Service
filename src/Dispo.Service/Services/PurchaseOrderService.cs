using Dispo.Commom;
using Dispo.Domain.DTOs.Request;
using Dispo.Domain.Entities;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;
using Domain.Enums;
using System.Transactions;

namespace Dispo.Service.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _PurchaseOrderRepository;
        private readonly IOrderRepository _OrderRepository;
        private readonly IAccountResolverService _AccountResolverService;

        public PurchaseOrderService(IPurchaseOrderRepository PurchaseOrderRepository, IOrderRepository OrderRepository, IAccountResolverService AccountResolverService)
        {
            this._PurchaseOrderRepository = PurchaseOrderRepository;
            this._OrderRepository = OrderRepository;
            this._AccountResolverService = AccountResolverService;
        }

        public long CreatePurchaseOrder(PurchaseOrderRequestDto PurchaseOrderRequestDto)
        {
            try
            {
                long purchaseOrderCreatedId = IDHelper.INVALID_ID;

                using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var currentWareHouse = _AccountResolverService.GetLoggedAccountId();

                    var purchaseOrder = new PurchaseOrder()
                    {
                        Number = PurchaseOrderRequestDto.Number,
                        CreationDate = PurchaseOrderRequestDto.CreationDate,
                        PaymentMethod = PurchaseOrderRequestDto.PaymentMethod,
                        NotificationType = PurchaseOrderRequestDto.NotificationType,
                        SupplierId = PurchaseOrderRequestDto.SupplierId,
                        WarehouseId = currentWareHouse,
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
    }
}
