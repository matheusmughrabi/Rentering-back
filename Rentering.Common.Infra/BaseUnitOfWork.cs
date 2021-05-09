using System;

namespace Rentering.Common.Infra
{
    public abstract class BaseUnitOfWork : IDisposable
    {
        private readonly RenteringDataContext _session;

        public BaseUnitOfWork(RenteringDataContext session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
