using SHARED.Contracts;
using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DbContext_MicroServices.Implementations
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly MicroServicesExample_DbContext _db;
        public OrderItemRepository(MicroServicesExample_DbContext db) : base(db)
        {
            _db = db; 
        }
    }
}
