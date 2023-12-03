using Dispo.Shared.Core.Domain.DTOs;

namespace Dispo.Movement.Core.Application.Interfaces
{
    public interface IMovementService
    {
        Task MoveProductAsync(ProductMovimentationDto productMovimentationDto);

        Task MoveBatchAsync(BatchMovimentationDto batchMovimentationDto);
    }
}