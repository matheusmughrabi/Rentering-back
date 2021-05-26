using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Data.RespositoriesEF;
using Rentering.Accounts.Domain.Data.RespositoriesEF.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Accounts.InfraEF.Data.Repositories
{
    public class AccountQueryRepositoryEF : IAccountQueryRepositoryEF
    {
        private readonly AccountsDbContext _accountsDbContext;

        public AccountQueryRepositoryEF(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;
        }

        public GetAccountQueryResultEF GetAccountById(int id)
        {
            var accountRetrieved =  _accountsDbContext.Account
                 .AsNoTracking()
                 .Where(c => c.Id == id)
                 .Select(c => new { c.Email.Email, c.Username.Username })
                 .FirstOrDefault();

            var accountQueryResult = new GetAccountQueryResultEF()
            {
                Email = accountRetrieved.Email,
                Username = accountRetrieved.Username
            };

            return accountQueryResult;
        }

        public IEnumerable<GetAccountQueryResult_AdminUsageOnlyEF> GetAllAccounts_AdminUsageOnly()
        {
            var accountRetrieved = _accountsDbContext.Account
                .AsNoTracking()
                .ToList();

            var accountQueryResults = new List<GetAccountQueryResult_AdminUsageOnlyEF>();
            accountRetrieved.ForEach(c => accountQueryResults.Add(new GetAccountQueryResult_AdminUsageOnlyEF()
            {
                Id = c.Id,
                Email = c.Email.Email,
                Username = c.Username.Username,
                Password = c.Password.Password,
                Role = c.Role
            }));

            return accountQueryResults;
        }
    }
}
