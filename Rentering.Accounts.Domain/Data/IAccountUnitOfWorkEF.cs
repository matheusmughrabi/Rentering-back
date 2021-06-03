using Rentering.Accounts.Domain.Data.RespositoriesEF;
using System;

namespace Rentering.Accounts.Domain.Data
{
    public interface IAccountUnitOfWorkEF : IDisposable
    {
        IAccountCUDRepositoryEF AccountCUDRepositoryEF { get; }
        IAccountQueryRepositoryEF AccountQueryRepositoryEF { get; }

        void Save();
    }
}
