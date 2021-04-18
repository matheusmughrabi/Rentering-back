using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System;
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

        public GetTenantQueryResult GetTenantById(int id)
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
