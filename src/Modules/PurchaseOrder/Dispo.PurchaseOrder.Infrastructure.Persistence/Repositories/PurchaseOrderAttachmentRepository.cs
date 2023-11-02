using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Dispo.Shared.Infrastructure.Persistence.Repositories;

namespace Dispo.PurchaseOrder.Infrastructure.Persistence.Repositories
{
    public class PurchaseOrderAttachmentRepository : BaseRepository<PurchaseOrderAttachment>, IPurchaseOrderAttachmentRepository
    {
        public PurchaseOrderAttachmentRepository(DispoContext dispoContext) : base(dispoContext)
        {
        }
    }
}