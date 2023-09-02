namespace Dispo.Domain.DTOs
{
    public class BatchInputDto
    {
        public required string Batch { get; set; }
        public required string FabricationDate { get; set; }
        public required string ExpirationDate { get; set; }
        public required int Quantity { get; set; }
        public required int OrderId { get; set; }
    }
}