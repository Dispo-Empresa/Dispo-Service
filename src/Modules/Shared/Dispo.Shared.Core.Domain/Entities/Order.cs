﻿namespace Dispo.Shared.Core.Domain.Entities
{
    public class Order : EntityBase
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public long PurchaseOrderId { get; set; }
        public long ProductId { get; set; }

        public Product Product { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public IList<Batch> Batches { get; set; }
    }
}