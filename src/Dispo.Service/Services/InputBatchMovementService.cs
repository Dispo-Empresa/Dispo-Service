using Dispo.Domain.DTOs;
using Dispo.Service.Services.Interfaces;

namespace Dispo.Service.Services
{
    public class InputBatchMovementService : IInputBatchMovementService
    {
        private readonly IMovementService _movementService;
        /*
            Movimentação de Entrada
            - Adicionar um Movement em memória.
            - Para cada lote enviado: [
	            Criar um BatchMovement.
	            Incrementar a Quantity do Movement.
	            Decrementar a Quantity do Batch.
            ]
        */
        public Task MoveAsync(BatchMovimentationDto batchMovimentationDto)
        {
            throw new NotImplementedException();
        }
    }
}
