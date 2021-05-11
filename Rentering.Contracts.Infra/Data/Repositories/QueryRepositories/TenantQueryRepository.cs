using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class TenantQueryRepository : ITenantQueryRepository
    {
        private readonly RenteringDataContext _context;

        public TenantQueryRepository(RenteringDataContext context)
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
	                        ELSE CAST(0 AS BIT);";

            var accountExists = _context.Connection.Query<bool>(
                    sql,
                    new
                    {
                        Id = accountId
                    }).FirstOrDefault();

            return accountExists;
        }

        public IEnumerable<GetTenantQueryResult> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public GetTenantQueryResult GetById(int id)
        {
            var renterFromDb = _context.Connection.Query<GetTenantQueryResult>(
                   "sp_Tenants_Query_GetTenantById",
                   new { Id = id },
                   commandType: CommandType.StoredProcedure
               ).FirstOrDefault();

            return renterFromDb;
        }

        public IEnumerable<GetTenantQueryResult> GetTenantProfilesOfCurrentUser(int accountId)
        {
            var rentersFromDb = _context.Connection.Query<GetTenantQueryResult>(
                    "sp_Tenants_Query_GetTenantsOfAccount",
                    new { AccountId = accountId },
                    commandType: CommandType.StoredProcedure
                );

            return rentersFromDb;
        }
    }
}
