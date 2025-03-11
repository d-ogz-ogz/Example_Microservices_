using SHARED.Contracts;
using SHARED.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DbContext_MicroServices.Implementations
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private readonly MicroServicesExample_DbContext _db;
        public StockRepository(MicroServicesExample_DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
