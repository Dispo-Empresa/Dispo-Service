using Dispo.PurchaseOrder.Core.Application.Interfaces;
using Dispo.PurchaseOrder.Core.Application.Models;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Utils;
using System.Transactions;

namespace Dispo.PurchaseOrder.Core.Application.Services
{
    public class PurchaseOrderAttachmentService : IPurchaseOrderAttachmentService
    {
        private readonly IPurchaseOrderAttachmentRepository _purchaseOrderAttachmentRepository;

        public PurchaseOrderAttachmentService(IPurchaseOrderAttachmentRepository purchaseOrderAttachmentRepository)
        {
            _purchaseOrderAttachmentRepository = purchaseOrderAttachmentRepository;
        }

        public long CreatePurchaseOrderAttachment(PurchaseOrderAttachmentRequestModel PurchaseOrderAttachment)
        {
            long purchaseOrderAttachmentId = IDHelper.INVALID_ID;

            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var purchaseOrderAttachment = new Shared.Core.Domain.Entities.PurchaseOrderAttachment
                {
                    PurchaseOrderId = PurchaseOrderAttachment.PurchaseOrderId,
                    Attatchment = PurchaseOrderAttachment.Attatchment,
                    CreationDate = PurchaseOrderAttachment.CreationDate,
                    ModifieldDate = PurchaseOrderAttachment.ModifieldDate
                };

                _purchaseOrderAttachmentRepository.Create(purchaseOrderAttachment);
                tc.Complete();
                purchaseOrderAttachmentId = purchaseOrderAttachment.Id;
            }

            return purchaseOrderAttachmentId;
        }
    }
}