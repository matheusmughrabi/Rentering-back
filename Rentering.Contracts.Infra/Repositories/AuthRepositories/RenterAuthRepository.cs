using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using System;
using System.Collections.Generic;
using System.Data;

namespace Rentering.Contracts.Infra.Repositories.AuthRepositories
{
    public class RenterAuthRepository : IRenterAuthRepository
    {
        private readonly RenteringDataContext _context;

        public RenterAuthRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public IEnumerable<AuthRenterProfilesOfTheCurrentUserQueryResults> GetRenterProfilesOfTheCurrentUser(int accountId)
        {
            var rentersFromDb = _context.Connection.Query<AuthRenterProfilesOfTheCurrentUserQueryResults>(
                    "sp_Renters_Auth_GetRentersIdsOfAccount",
                    new { AccountId = accountId },
                    commandType: CommandType.StoredProcedure
                );

            return rentersFromDb;
        }
    }
}
