using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Enums;

namespace Dispo.PurchaseOrder.Core.Application.Models
{
    public class PurchaseOrderRequestModel
    {
        public string Number { get; set; }
        public DateTime CreationDate { get; set; }
        public ePaymentMethod PaymentMethod { get; set; }
        public eNotificationType NotificationType { get; set; }
        public long SupplierId { get; set; }
        public IList<OrderInfoDto> Orders { get; set; }
    }
}