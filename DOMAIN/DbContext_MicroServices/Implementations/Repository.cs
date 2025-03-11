using Microsoft.EntityFrameworkCore;
using SHARED.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.DbContext_MicroServices.Implementations
{
  
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly MicroServicesExample_DbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(MicroServicesExample_DbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
            
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }
        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
           await _db.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _db.Update(entity);
        }
    }
}
