using Newtonsoft.Json;

namespace Dispo.Shared.Core.Domain.DTOs
{
    public class BatchDetailsDto
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        public long ProductId { get; set; }
    }
}