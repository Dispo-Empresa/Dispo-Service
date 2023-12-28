using Dispo.Shared.Core.Domain.Enums;

namespace Dispo.Shared.Core.Domain.DTOs
{
    public class MovimentationDetailsDto
    {
        public long Id { get; set; }
        public eMovementType Type { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
    }
}
