using Dispo.Domain.DTOs;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;

namespace Dispo.Service.Services
{
    public class PurschaseOrderService : IPurschaseOrderService
    {
        private readonly IPurschaseOrderRepository _purschaseOrderRepository;

        public PurschaseOrderService(IPurschaseOrderRepository purschaseOrderRepository)
        {
            _purschaseOrderRepository = purschaseOrderRepository;
        }

        public List<PurschaseOrderDto> GetByProcuctId(long productId)
        {
            return _purschaseOrderRepository.GetByProcuctId(productId);
        }
    }
}