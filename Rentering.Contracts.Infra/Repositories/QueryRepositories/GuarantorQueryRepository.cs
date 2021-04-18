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
    public class GuarantorQueryRepository : IGuarantorQueryRepository
    {
        private readonly RenteringDataContext _context;

        public GuarantorQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public GetGuarantorQueryResult GetGuarantorById(int id)
        {
            var guarantorFromDb = _context.Connection.Query<GetGuarantorQueryResult>(
                   "sp_Guarantors_Query_GetGuarantorById",
                   new { Id = id },
                   commandType: CommandType.StoredProcedure
               ).FirstOrDefault();

            return guarantorFromDb;
        }

        public IEnumerable<GetGuarantorQueryResult> GetGuarantorProfilesOfCurrentUser(int accountId)
        {
            var guarantorsFromDb = _context.Connection.Query<GetGuarantorQueryResult>(
                    "sp_Guarantors_Query_GetGuarantorsOfAccount",
                    new { AccountId = accountId },
                    commandType: CommandType.StoredProcedure
                );

            return guarantorsFromDb;
        }
    }
}
