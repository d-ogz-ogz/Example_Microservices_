using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Events
{
    public class PaymentNotCompletedEvent
    {
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid? RelatedOrderId { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
