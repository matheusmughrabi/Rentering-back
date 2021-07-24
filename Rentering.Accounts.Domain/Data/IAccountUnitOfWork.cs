using Rentering.Accounts.Domain.Data.Repositories;
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
