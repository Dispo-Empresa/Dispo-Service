namespace Dispo.Domain.Entities
{
    public class UserWarehouse
    {
        public UserWarehouse(long warehouseId, long userId)
        {
            WarehouseId = warehouseId;
            UserId = userId;
        }

        public UserWarehouse()
        {
        }

        public long WarehouseId { get; set; }
        public long UserId { get; set; }


        public Warehouse Warehouse { get; set; }
        public User User { get; set; }
    }
}
