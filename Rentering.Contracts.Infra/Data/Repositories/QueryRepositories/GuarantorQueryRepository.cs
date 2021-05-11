using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class GuarantorQueryRepository : IGuarantorQueryRepository
    {
        private readonly RenteringDataContext _context;

        public GuarantorQueryRepository(RenteringDataContext context)
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
                    new { 
                        Id = accountId 
                    }).FirstOrDefault();

            return accountExists;
        }

        public IEnumerable<GetGuarantorQueryResult> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public GetGuarantorQueryResult GetById(int id)
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
