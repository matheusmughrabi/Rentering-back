using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.QueryRepositories
{
    public class RenterQueryRepository : IRenterQueryRepository
    {
        private readonly RenteringDataContext _context;

        public RenterQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public GetRenterQueryResult GetRenterById(int id)
        {
            var renterFromDb = _context.Connection.Query<GetRenterQueryResult>(
                    "sp_Renters_Query_GetRenterById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return renterFromDb;
        }

        public IEnumerable<GetRenterQueryResult> GetRenterProfilesOfCurrentUser(int accountId)
        {
            var rentersFromDb = _context.Connection.Query<GetRenterQueryResult>(
                    "sp_Renters_Query_GetRentersOfAccount",
                    new { AccountId = accountId },
                    commandType: CommandType.StoredProcedure
                );

            return rentersFromDb;
        }
    }
}
