using Rentering.Accounts.Domain.Data.CUDRepositories;
using Rentering.Accounts.Domain.Data.QueryRepositories;
using System;

namespace Rentering.Accounts.Domain.Data
{
    public interface IAccountUnitOfWork : IDisposable
    {
        IAccountCUDRepository AccountCUDRepository { get; }
        IAccountQueryRepository AccountQueryRepository { get; }

        void Save();
    }
}
