namespace Dispo.Shared.Core.Domain.Entities
{
    public class BatchMovement : Base
    {
        public long MovementId { get; set; }
        public long BatchId { get; set; }

        public Movement Movement { get; set; }
        public Batch Batch { get; set; }
    }
}