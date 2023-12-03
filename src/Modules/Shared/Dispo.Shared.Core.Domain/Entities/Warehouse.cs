namespace Dispo.Shared.Core.Domain.Entities
{
    public class Warehouse : EntityBase
    {
        public bool Ativo { get; set; }
        public string Name { get; set; }
        public long AddressId { get; set; }

        public Address Address { get; set; }
        public IList<Movement> Movements { get; set; }
        public IList<PurchaseOrder> PurchaseOrders { get; set; }
        public IList<WarehouseAccount> WarehouseAccounts { get; set; }
    }
}