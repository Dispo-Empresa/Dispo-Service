using Dispo.Domain.DTOs;
using Dispo.Domain.DTOs.RequestDTOs;
using Dispo.Domain.Entities;
using Dispo.Domain.Enums;

namespace Dispo.Service.Services.Interfaces
{
    public interface IMovementService
    {
        Task MoveProductAsync(ProductMovimentationDto productMovimentationDto);
        Task MoveBatchAsync(BatchMovimentationDto batchMovimentationDto);
    }
}