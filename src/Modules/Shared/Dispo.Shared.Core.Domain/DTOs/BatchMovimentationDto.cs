using Dispo.Shared.Core.Domain.Enums;

namespace Dispo.Shared.Core.Domain.DTOs
{
    public class BatchMovimentationDto
    {
        public List<BatchDetailsDto> Batches { get; set; }
        public eMovementType MovementType { get; set; }
    }
}