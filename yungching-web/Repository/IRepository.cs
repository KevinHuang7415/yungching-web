using System;
using System.Linq;
using System.Linq.Expressions;

namespace yungching_web.Repository
{
    public interface IRepository<T>
    {
        void Create(T entity);

        T Read(Expression<Func<T, bool>> predicate);

        IQueryable<T> Reads();

        void Update(T entity);

        void Delete(T entity);

        void SaveChanges();
    }
}