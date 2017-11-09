using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace webapi.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(long id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        T Remove(T entity);
        T RemoveById(long id);
        IEnumerable<T> RemoveRange(IEnumerable<T> entities);
    }
}