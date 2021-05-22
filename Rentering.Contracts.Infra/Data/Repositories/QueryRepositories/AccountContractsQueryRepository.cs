using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class AccountContractsQueryRepository : IAccountContractsQueryRepository
    {
        private readonly RenteringDataContext _context;

        public AccountContractsQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfAccountExists(int accountId)
        {
            var sql = @"SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [Accounts]
		                        WHERE [Id] = @Id
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT)
                            END;";

            var accountExists = _context.Connection.Query<bool>(
                    sql,
                    new
                    {
                        Id = accountId
                    }).FirstOrDefault();

            return accountExists;
        }

        public IEnumerable<GetAccountContractsQueryResults> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public GetAccountContractsQueryResults GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
