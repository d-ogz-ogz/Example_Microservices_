using SHARED.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; } 
        public bool IsActive { get; set; }
        public virtual List<OrderItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(i => i.Quantity * i.UnitPrice);
    }
}
