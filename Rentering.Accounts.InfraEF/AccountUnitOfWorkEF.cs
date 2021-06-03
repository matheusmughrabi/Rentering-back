using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Data.RespositoriesEF;
using Rentering.Accounts.InfraEF.Data.Repositories;

namespace Rentering.Accounts.InfraEF
{
    public class AccountUnitOfWorkEF : IAccountUnitOfWorkEF
    {
        private readonly AccountsDbContext _accountsDbContext;

        public AccountUnitOfWorkEF(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;

            AccountCUDRepositoryEF = new AccountCUDRepositoryEF(_accountsDbContext);
            AccountQueryRepositoryEF = new AccountQueryRepositoryEF(_accountsDbContext);
        }

        public IAccountCUDRepositoryEF AccountCUDRepositoryEF { get; private set; }
        public IAccountQueryRepositoryEF AccountQueryRepositoryEF { get; private set; }

        public void Dispose()
        {
            _accountsDbContext?.Dispose();
        }

        public void Save()
        {
            _accountsDbContext.SaveChanges();
        }
    }
}
