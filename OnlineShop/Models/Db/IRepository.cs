using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models.Db
{
    interface IRepository : IDisposable
    {
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
