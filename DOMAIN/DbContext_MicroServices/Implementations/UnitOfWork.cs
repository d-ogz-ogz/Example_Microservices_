using SHARED.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DbContext_MicroServices.Implementations
{

    public class UnitOfWork : IDisposable, IUnitOfWork

    {
        private readonly MicroServicesExample_DbContext _db;
        public UnitOfWork(MicroServicesExample_DbContext db)
        {
            Stock_Details = new StockRepository(_db);
            Orders = new OrderReposoitory(_db);
            OrderItems = new OrderItemRepository(_db);
        }

        public IStockRepository Stock_Details { get; set; }

        public IOrderRepository Orders{ get; set; }
        public IOrderItemRepository OrderItems{ get; set; }

        public void Dispose()
        {
           _db.Dispose();
        }
    }
}
