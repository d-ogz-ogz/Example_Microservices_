using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Events
{
    public class StockRollBackEvent
    {
        public Guid OrderId { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public decimal Amount { get; set; }
        public string? CustomerEmail { get; set; }

    }
}
