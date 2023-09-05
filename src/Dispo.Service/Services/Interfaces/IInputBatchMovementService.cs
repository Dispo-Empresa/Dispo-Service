using Dispo.Domain.DTOs;

namespace Dispo.Service.Services.Interfaces
{
    public interface IInputBatchMovementService
    {
        Task MoveAsync(BatchMovimentationDto batchMovimentationDto);
    }
}
