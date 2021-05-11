using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Common.Shared.Data;

namespace Rentering.Accounts.Domain.Data
{
    public interface IAccountUnitOfWork : IUnitOfWork
    {
        IAccountCUDRepository AccountCUD { get; }
        IAccountQueryRepository AccountQuery { get; }
    }
}
