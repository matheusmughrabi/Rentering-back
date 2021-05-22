using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Common.Infra;

namespace Rentering.Accounts.Infra.Data
{
    public class AccountUnitOfWork : BaseUnitOfWork, IAccountUnitOfWork
    {
        public AccountUnitOfWork(
            RenteringDataContext renteringDataContext,
            IAccountCUDRepository accountCUD, 
            IAccountQueryRepository accountQuery) : base(renteringDataContext)
        {
            AccountCUD = accountCUD;
            AccountQuery = accountQuery;
        }

        public IAccountCUDRepository AccountCUD { get; }
        public IAccountQueryRepository AccountQuery { get; }
    }
}
