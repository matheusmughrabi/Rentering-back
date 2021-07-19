using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Data.QueryRepositories;
using Rentering.Accounts.Domain.Data.QueryRepositories.QueryResults;
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
    }
}
