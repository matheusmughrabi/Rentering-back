using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Data.RespositoriesEF;
using Rentering.Infra.Accounts.CUDRepositories;
using Rentering.Infra.Accounts.QueryRepositories;

namespace Rentering.Infra.Accounts
{
    public class AccountUnitOfWorkEF : IAccountUnitOfWorkEF
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountUnitOfWorkEF(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;

            AccountCUDRepositoryEF = new AccountCUDRepositoryEF(_renteringDbContext);
            AccountQueryRepositoryEF = new AccountQueryRepositoryEF(_renteringDbContext);
        }

        public IAccountCUDRepositoryEF AccountCUDRepositoryEF { get; private set; }
        public IAccountQueryRepositoryEF AccountQueryRepositoryEF { get; private set; }

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
