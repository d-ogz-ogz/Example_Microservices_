using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.models
{
    public class Stock
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public int Count { get; set; } 
        public Item? Item { get; set; }  
    }
}
