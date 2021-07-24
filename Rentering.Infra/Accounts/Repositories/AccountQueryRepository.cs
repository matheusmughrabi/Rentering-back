using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Data.Repositories;
using Rentering.Accounts.Domain.Data.Repositories.QueryResults;
using System.Linq;

namespace Rentering.Infra.Accounts.Repositories
{
    public class AccountQueryRepository : IAccountQueryRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountQueryRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public GetAccountQueryResult GetAccountById(int id)
        {
            var accountRetrieved = _renteringDbContext.Account
                 .AsNoTracking()
                 .Where(c => c.Id == id)
                 .Select(c => new { c.Email.Email, c.Username.Username })
                 .FirstOrDefault();

            if (accountRetrieved == null)
                return null;

            var accountQueryResult = new GetAccountQueryResult()
            {
                Email = accountRetrieved.Email,
                Username = accountRetrieved.Username
            };

            return accountQueryResult;
        }
    }
}
