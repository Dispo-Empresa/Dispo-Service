using Dispo.Shared.Core.Domain.Enums;

namespace Dispo.Shared.Core.Domain.Entities
{
    public class Batch : EntityBase
    {
        public string Key { get; set; }
        public int ProductQuantity { get; set; }
        public int QuantityPerBatch { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long OrderId { get; set; }

        public Order Order { get; set; }
        public IList<BatchMovement> BatchMovements { get; set; }

        public void IncreaseOrDecreaseQuantityByMovementType(eMovementType movementType, int quantity, DateTime? expirationDate = null)
        {
            if (movementType is eMovementType.Input)
                QuantityPerBatch += quantity;
            else
                QuantityPerBatch -= quantity;

            ExpirationDate = expirationDate.GetValueOrDefault(DateTime.Now);
        }
    }
}