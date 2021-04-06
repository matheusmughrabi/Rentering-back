using Dapper;
using Rentering.Accounts.Application.QueryResults;
using Rentering.Accounts.Domain.Repositories.QueryRepositories;
using Rentering.Common.Infra;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Accounts.Infra.Repositories.QueryRepositories
{
    public class AccountQueryRepository : IAccountQueryRepository
    {
        private readonly RenteringDataContext _context;

        public AccountQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public GetAccountQueryResult GetAccountById(int id)
        {
            var accountQueryResult = _context.Connection.Query<GetAccountQueryResult>(
                    "SELECT Id, FirstName, LastName, Email, Username, Role FROM RenteringUsers where Id = @Id",
                    new { Id = id }
                ).FirstOrDefault();

            return accountQueryResult;
        }

        public IEnumerable<GetAccountQueryResult> GetAccounts()
        {
            var accountsQueryResult = _context.Connection.Query<GetAccountQueryResult>(
                    "SELECT Id, FirstName, LastName, Email, Username, Role FROM RenteringUsers");

            return accountsQueryResult;
        }
    }
}
