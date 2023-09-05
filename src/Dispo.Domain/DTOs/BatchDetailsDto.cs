namespace Dispo.Domain.DTOs
{
    public class BatchDetailsDto
    {
        public required string Batch { get; set; }
        public DateTime? FabricationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public required int Quantity { get; set; }
        public int? OrderId { get; set; }
    }
}