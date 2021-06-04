using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Data.CUDRepositories;
using Rentering.Accounts.Domain.Data.QueryRepositories;
using Rentering.Infra.Accounts.CUDRepositories;
using Rentering.Infra.Accounts.QueryRepositories;

namespace Rentering.Infra.Accounts
{
    public class AccountUnitOfWork : IAccountUnitOfWork
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountUnitOfWork(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;

            AccountCUDRepository = new AccountCUDRepository(_renteringDbContext);
            AccountQueryRepository = new AccountQueryRepository(_renteringDbContext);
        }

        public IAccountCUDRepository AccountCUDRepository { get; private set; }
        public IAccountQueryRepository AccountQueryRepository { get; private set; }

        public void Dispose()
        {
            _renteringDbContext?.Dispose();
        }

        public void Save()
        {
            _renteringDbContext.SaveChanges();
        }
    }
}
