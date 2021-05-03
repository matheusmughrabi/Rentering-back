using Dapper;
using Rentering.Accounts.Application.QueryResults;
using Rentering.Accounts.Domain.Repositories.QueryRepositories;
using Rentering.Common.Infra;
using System.Collections.Generic;
using System.Data;
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

        public bool CheckIfEmailExists(string email)
        {
            var emailExists = _context.Connection.Query<bool>(
                     "sp_Accounts_Query_CheckIfEmailExists",
                     new { Email = email },
                     commandType: CommandType.StoredProcedure
                 ).FirstOrDefault();

            return emailExists;
        }

        public bool CheckIfUsernameExists(string username)
        {
            var documentExists = _context.Connection.Query<bool>(
                    "sp_Accounts_Query_CheckIfUsernameExists",
                    new { Username = username },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return documentExists;
        }

        public GetAccountQueryResult GetAccountById(int id)
        {
            var accountQueryResult = _context.Connection.Query<GetAccountQueryResult>(
                    "sp_Accounts_Query_GetAllAccounts",
                    new { Id = id }
                ).FirstOrDefault();

            return accountQueryResult;
        }

        public IEnumerable<GetAccountQueryResult> GetAccounts()
        {
            var accountsFromDb = _context.Connection.Query<GetAccountQueryResult>(
                    "sp_Accounts_Query_GetAllAccounts",
                    commandType: CommandType.StoredProcedure);

            return accountsFromDb;
        }
    }
}
