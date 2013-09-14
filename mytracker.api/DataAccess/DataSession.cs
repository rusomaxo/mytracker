using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace mytracker.api.DataAccess
{
    public class DataSession : IDataSession
    {
        private readonly ISession _session;

        public DataSession(ISession session)
        {
            _session = session;
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public void SaveOrUpdate(object obj)
        {
            _session.SaveOrUpdate(obj);
        }

        public IDataTransaction BeginTransaction()
        {
            return new DataTransaction(_session.BeginTransaction());
        }

        public IDataTransaction BeginTransaction(IsolationLevel level)
        {
            return new DataTransaction(_session.BeginTransaction(level));
        }
    }
}
