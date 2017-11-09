using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace webapi.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly DbContext mContext;
        protected readonly DbSet<T> mEntities;

        public Repository(DbContext context)
        {
            mContext = context;
            mEntities = mContext.Set<T>();
        }

        public void Add(T entity)
        {
            mEntities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            mEntities.AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return mEntities.Where(predicate);
        }

        public T Get(long id)
        {
            return mEntities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return mEntities.ToArray();
        }

        public T Remove(T entity)
        {
            return mEntities.Remove(entity);
        }

        public T RemoveById(long id)
        {
            var entity = mEntities.Find(id);
            return mEntities.Remove(entity);
        }

        public IEnumerable<T> RemoveRange(IEnumerable<T> entities)
        {
            return mEntities.RemoveRange(entities);
        }
    }
}