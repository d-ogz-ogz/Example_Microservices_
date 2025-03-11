using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Events
{
    public class StockCheckEvent
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
    
    }
}
