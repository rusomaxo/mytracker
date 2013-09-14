using System.Data;
using System.Linq;

namespace mytracker.api.DataAccess
{
    public interface IDataSession
    {
        IQueryable<T> Query<T>();
        void SaveOrUpdate(object obj);
        IDataTransaction BeginTransaction();
        IDataTransaction BeginTransaction(IsolationLevel level);
    }
}