using System.Data;
using NHibernate;

namespace mytracker.api.DataAccess
{
    public class DataTransaction : IDataTransaction
    {
        private readonly ITransaction _transaction;

        public DataTransaction(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Commit();
        }

        public bool WasCommitted
        {
            get { return _transaction.WasCommitted; }
        }

        public bool WasRollbacked
        {
            get { return _transaction.WasRolledBack; }
        }
    }
}
