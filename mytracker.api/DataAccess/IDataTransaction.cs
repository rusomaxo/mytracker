using System.Data;

namespace mytracker.api.DataAccess
{
    public interface IDataTransaction
    {
        void Commit();

        void Rollback();

        bool WasCommitted { get; }

        bool WasRollbacked { get; }
    }
}
