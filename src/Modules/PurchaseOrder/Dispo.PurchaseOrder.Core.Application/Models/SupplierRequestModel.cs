namespace Dispo.PurchaseOrder.Core.Application.Models
{
    public class SupplierRequestModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressRequestModel Address { get; set; }
    }
}