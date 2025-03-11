using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Contracts
{
    public interface IOrderItemRepository:IRepository<OrderItem>
    {
    }
}
