using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Data.RespositoriesEF;
using Rentering.Accounts.Domain.Entities;
using System.Linq;

namespace Rentering.Accounts.InfraEF.Data.Repositories
{
    public class AccountCUDRepositoryEF : IAccountCUDRepositoryEF
    {
        private readonly AccountsDbContext _accountsDbContext;

        public AccountCUDRepositoryEF(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;
        }

        public AccountEntity GetAccountForCUD(int id)
        {
            var accountEntity = _accountsDbContext.Account
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return accountEntity;
        }

        public AccountEntity GetAccountForLogin(string username)
        {
            var accountEntity = _accountsDbContext.Account
               .AsNoTracking()
               .Where(c => c.Username.Username == username)
               .FirstOrDefault();

            return accountEntity;
        }

        public bool EmailExists(string email)
        {
            var emailExists = _accountsDbContext.Account
                .AsNoTracking()
                .Any(c => c.Email.Email == email);

            return emailExists;
            
        }

        public bool UsernameExists(string username)
        {
            var usernameExists = _accountsDbContext.Account
                .AsNoTracking()
                .Any(c => c.Username.Username == username);

            return usernameExists;
        }

        public AccountEntity Add(AccountEntity accountEntity)
        {
            if (accountEntity == null)
                return null;

            var addedAccountEntity = _accountsDbContext.Account.Add(accountEntity).Entity;
            return addedAccountEntity;
        }

        public AccountEntity Delete(AccountEntity accountEntity)
        {
            if (accountEntity == null)
                return null;

            var deletedAccount = _accountsDbContext.Remove(accountEntity).Entity;
            return deletedAccount;
        }

        public AccountEntity Delete(int id)
        {
            var accountEntity = _accountsDbContext.Account
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (accountEntity == null)
                return null;

            var deletedAccount = _accountsDbContext.Remove(accountEntity).Entity;
            return deletedAccount;
        }
    }
}
