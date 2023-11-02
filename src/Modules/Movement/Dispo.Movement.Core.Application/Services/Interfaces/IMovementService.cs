using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Movement.Core.Application.Services.Interfaces
{
    public interface IMovementService
    {
        Task MoveProductAsync(ProductMovimentationDto productMovimentationDto);

        Task MoveBatchAsync(BatchMovimentationDto batchMovimentationDto);
    }
}