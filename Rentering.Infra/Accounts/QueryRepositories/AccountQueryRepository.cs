using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Data.QueryRepositories;
using Rentering.Accounts.Domain.Data.QueryRepositories.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Infra.Accounts.QueryRepositories
{
    public class AccountQueryRepository : IAccountQueryRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountQueryRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public GetAccountQueryResultEF GetAccountById(int id)
        {
            var accountRetrieved = _renteringDbContext.Account
                 .AsNoTracking()
                 .Where(c => c.Id == id)
                 .Select(c => new { c.Email.Email, c.Username.Username })
                 .FirstOrDefault();

            if (accountRetrieved == null)
                return null;

            var accountQueryResult = new GetAccountQueryResultEF()
            {
                Email = accountRetrieved.Email,
                Username = accountRetrieved.Username
            };

            return accountQueryResult;
        }

        public IEnumerable<GetAccountQueryResult_AdminUsageOnlyEF> GetAllAccounts_AdminUsageOnly()
        {
            var accountRetrieved = _renteringDbContext.Account
                .AsNoTracking()
                .ToList();

            var accountQueryResults = new List<GetAccountQueryResult_AdminUsageOnlyEF>();
            accountRetrieved?.ForEach(c => accountQueryResults.Add(new GetAccountQueryResult_AdminUsageOnlyEF()
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
