namespace Dispo.Product.Core.Application.Models
{
    public class WarehouseRequestModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string UF { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}