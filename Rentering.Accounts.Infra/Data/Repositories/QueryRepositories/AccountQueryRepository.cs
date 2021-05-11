using Dapper;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Common.Infra;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Accounts.Infra.Data.Repositories.QueryRepositories
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
            var sql = @"SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [Accounts]
		                        WHERE [Email] = @Email
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT);";

            var emailExists = _context.Connection.Query<bool>(
                     sql,
                     new { Email = email }).FirstOrDefault();

            return emailExists;
        }

        public bool CheckIfUsernameExists(string username)
        {
            var sql = @"SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [Accounts]
		                        WHERE [Username] = @Username
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT);";

            var documentExists = _context.Connection.Query<bool>(
                    sql,
                    new { Username = username }).FirstOrDefault();

            return documentExists;
        }

        public GetAccountQueryResult GetAccountById(int id)
        {
            var sql = @"SELECT Email, Username FROM Accounts WHERE Id = @Id;";

            var accountQueryResult = _context.Connection.Query<GetAccountQueryResult>(
                    sql,
                    new { Id = id }
                ).FirstOrDefault();

            return accountQueryResult;
        }

        public GetAccountForLoginQueryResult GetAccountForLogin(string username)
        {
            var sql = @"SELECT Id, Email, Username, Password, Role FROM Accounts WHERE Username = @Username;";

            var accountFromDb = _context.Connection.Query<GetAccountForLoginQueryResult>(
                    sql,
                    new { Username = username }).FirstOrDefault();

            return accountFromDb;
        }

        public IEnumerable<GetAccountQueryResult_AdminUsageOnly> GetAllAccounts_AdminUsageOnly()
        {
            var sql = @"SELECT Id, Email, Username, Password, Role FROM Accounts;";

            var accountsFromDb = _context.Connection.Query<GetAccountQueryResult_AdminUsageOnly>(
                    sql);

            return accountsFromDb;
        }
    }
}
