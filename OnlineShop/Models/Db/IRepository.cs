using System;
using System.Collections.Generic;

namespace OnlineShop.Models.Db
{
    internal interface IRepository : IDisposable
    {//interface for using our COntext in right way
        T Insert<T>(T item, bool saveNow)
            where T : class;

        T Update<T>(T item, bool saveNow)
            where T : class;

        T Delete<T>(T item, bool saveNow)
            where T : class;

        int Save();

        IEnumerable<T> Select<T>()
            where T : class;
    }
}