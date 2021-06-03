using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Data.RespositoriesEF;
using Rentering.Accounts.Domain.Entities;
using System.Linq;

namespace Rentering.Infra.Accounts.CUDRepositories
{
    public class AccountCUDRepositoryEF : IAccountCUDRepositoryEF
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountCUDRepositoryEF(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public AccountEntity GetAccountForCUD(int id)
        {
            var accountEntity = _renteringDbContext.Account
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return accountEntity;
        }

        public AccountEntity GetAccountForLogin(string username)
        {
            var accountEntity = _renteringDbContext.Account
               .AsNoTracking()
               .Where(c => c.Username.Username == username)
               .FirstOrDefault();

            return accountEntity;
        }

        public bool EmailExists(string email)
        {
            var emailExists = _renteringDbContext.Account
                .AsNoTracking()
                .Any(c => c.Email.Email == email);

            return emailExists;

        }

        public bool UsernameExists(string username)
        {
            var usernameExists = _renteringDbContext.Account
                .AsNoTracking()
                .Any(c => c.Username.Username == username);

            return usernameExists;
        }

        public AccountEntity Add(AccountEntity accountEntity)
        {
            if (accountEntity == null)
                return null;

            var addedAccountEntity = _renteringDbContext.Account.Add(accountEntity).Entity;
            return addedAccountEntity;
        }

        public AccountEntity Delete(AccountEntity accountEntity)
        {
            if (accountEntity == null)
                return null;

            var deletedAccount = _renteringDbContext.Remove(accountEntity).Entity;
            return deletedAccount;
        }

        public AccountEntity Delete(int id)
        {
            var accountEntity = _renteringDbContext.Account
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (accountEntity == null)
                return null;

            var deletedAccount = _renteringDbContext.Remove(accountEntity).Entity;
            return deletedAccount;
        }
    }
}
