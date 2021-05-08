using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.QueryRepositories
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
            var accountExists = _context.Connection.Query<bool>(
                    "sp_Accounts_Query_CheckIfAccountExists",
                    new { Id = accountId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

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
