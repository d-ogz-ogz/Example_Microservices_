using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Item? Item { get; set; }
        public virtual Order Order { get; set; }
        public decimal OrderItemPrice=>Quantity*UnitPrice;
    }
}
