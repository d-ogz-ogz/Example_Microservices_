using SHARED.Enums;
using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerEmail { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
