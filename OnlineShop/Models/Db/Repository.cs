using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{
    public class Repository<TContext> : IRepository
        where TContext: AppContext, new()
    {
        public Repository()
        {
            Context = new TContext();
        }
        public TContext Context { get; private set; }

        public T Delete<T>(T item, bool saveNow) where T : class
        {
            Context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            if (saveNow)
                Context.SaveChanges();

            return item;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public T Insert<T>(T item, bool saveNow) where T : class
        {
            Context.Entry(item).State = System.Data.Entity.EntityState.Added;
            if (saveNow)
                Context.SaveChanges();

            return item;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public IEnumerable<T> Select<T>() where T : class
        {
            return Context.Set<T>();
        }

        public T Update<T>(T item, bool saveNow) where T : class
        {
            Context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            if (saveNow)
                Context.SaveChanges();

            return item;
        }
    }

    public class ContextRepository : Repository<AppContext>{}
}