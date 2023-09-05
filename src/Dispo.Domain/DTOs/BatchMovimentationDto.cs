using Dispo.Domain.Enums;

namespace Dispo.Domain.DTOs
{
    public class BatchMovimentationDto
    {
        public required List<BatchDetailsDto> Batches { get; set; }
        public eMovementType MovementType { get; set; }

        public void SetMovementType()
        {
            MovementType = Batches.Exists(w => w.OrderId == null) ? eMovementType.Output
                                                                  : eMovementType.Input;
        }
    }
}
