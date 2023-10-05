using Dispo.Domain.DTOs;

namespace Dispo.Service.Services.Interfaces
{
    public interface IPurschaseOrderService
    {
        List<PurschaseOrderDto> GetByProcuctId(long productId);
    }
}
