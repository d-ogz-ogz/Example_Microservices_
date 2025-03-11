using SHARED.Contracts;
using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DbContext_MicroServices.Implementations
{
    public class OrderReposoitory : Repository<Order>, IOrderRepository
    {
        private readonly MicroServicesExample_DbContext _db;
        public OrderReposoitory(MicroServicesExample_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
