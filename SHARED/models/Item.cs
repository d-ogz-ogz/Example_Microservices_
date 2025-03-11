﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.models
{
    public class Item
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }
        public virtual Stock Stock_Info { get; set; }

    }
}
