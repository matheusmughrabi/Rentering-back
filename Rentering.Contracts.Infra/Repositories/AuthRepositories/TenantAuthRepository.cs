using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using System.Collections.Generic;
using System.Data;

namespace Rentering.Contracts.Infra.Repositories.AuthRepositories
{
    public class TenantAuthRepository : ITenantAuthRepository
    {
        private readonly RenteringDataContext _context;

        public TenantAuthRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public IEnumerable<AuthTenantProfilesOfTheCurrentUserQueryResults> GetTenantProfilesOfTheCurrentUser(int accountId)
        {
            var rentersFromDb = _context.Connection.Query<AuthTenantProfilesOfTheCurrentUserQueryResults>(
                    "sp_Tenants_Auth_GetTenantsIdsOfAccount",
                    new { AccountId = accountId },
                    commandType: CommandType.StoredProcedure
                );

            return rentersFromDb;
        }
    }
}
