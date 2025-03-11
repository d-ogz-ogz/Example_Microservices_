using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Contracts
{
    public interface IRepository<T> where T : class, new()
    {
        void Add(T entity);
        void Update(T entity);
        Task SaveChangesAsync();
        Task<T> GetById(int id);
        void Delete(T entity);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null
            );

    }
}
