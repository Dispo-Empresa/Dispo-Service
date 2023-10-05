using Dispo.Domain.Enums;

namespace Dispo.Domain.DTOs
{
    public class BatchMovimentationDto
    {
        public required List<BatchDetailsDto> Batches { get; set; }
        public required eMovementType MovementType { get; set; }
    }
}
