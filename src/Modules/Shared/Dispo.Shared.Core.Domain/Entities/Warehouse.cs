namespace Dispo.Shared.Core.Domain.Entities
{
    public class Warehouse : EntityBase
    {
        public bool Ativo { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string UF { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public string AdditionalInfo { get; set; }

        public IList<Movement> Movements { get; set; }
        public IList<PurchaseOrder> PurchaseOrders { get; set; }
        public IList<WarehouseAccount> WarehouseAccounts { get; set; }
    }
}