using Newtonsoft.Json;

namespace Dispo.Domain.DTOs
{
    public class BatchDetailsDto
    {
        public long Id { get; set; }
        [JsonProperty("batch")]
        public required string Key { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public required int Quantity { get; set; }
        public int? OrderId { get; set; }
    }
}