using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Entities;
using System;
using System.Linq;

namespace Rentering.Accounts.InfraEF.Data.Repositories
{
    public class AccountCUDRepository : IAccountCUDRepository
    {
        private readonly AccountsDbContext _context;

        public AccountCUDRepository(AccountsDbContext context)
        {
            _context = context;
        }

        public AccountEntity GetAccountForCUD(int id)
        {
            var accountEntity = _context.Account
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return accountEntity;
        }

        public AccountEntity GetAccountForLoginCUD(string username)
        {
            var accountEntity = _context.Account
                .Where(c => c.Username.Username == username)
                .FirstOrDefault();

            return accountEntity;
        }

        public AccountEntity Create(AccountEntity entity)
        {
            throw new NotImplementedException();
        }

        public AccountEntity Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AccountEntity Update(int id, AccountEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
