using Dispo.Domain.Enums;

namespace Dispo.Domain.DTOs
{
    public class BatchMovimentationDto
    {
        public required List<BatchDetailsDto> Batches { get; set; }
        public eMovementType MovementType
        {
            get => Batches.Exists(w => w.OrderId == null) ? eMovementType.Output : eMovementType.Input;
        }
    }
}
