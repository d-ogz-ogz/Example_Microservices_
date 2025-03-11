using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Contracts
{
    public interface IUnitOfWork
    {
        IStockRepository Stock_Details { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        void Dispose();
    }
}
