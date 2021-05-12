using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class RenterQueryRepository : IRenterQueryRepository
    {
        private readonly RenteringDataContext _context;

        public RenterQueryRepository(RenteringDataContext context)
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

        public IEnumerable<GetRenterQueryResult> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public GetRenterQueryResult GetById(int id)
        {
            var sql = @"SELECT 
							Id,
                            AccountId,
                            Status,
                            FirstName,
                            LastName,
                            Nationality,
                            Ocupation,
                            MaritalStatus,
                            IdentityRG,
                            CPF,
                            Street,
                            SpouseFirstName,
                            SpouseLastName,
                            SpouseNationality,
                            SpouseIdentityRG,
                            SpouseCPF
						FROM 
							Renters
						WHERE 
							[Id] = @Id;";

            var renterFromDb = _context.Connection.Query<GetRenterQueryResult>(
                    sql,
                    new { Id = id }).FirstOrDefault();

            return renterFromDb;
        }

        public IEnumerable<GetRenterQueryResult> GetRenterProfilesOfCurrentUser(int accountId)
        {
            var sql = @"SELECT 
							Id,
                            AccountId,
                            Status,
                            FirstName,
                            LastName,
                            Nationality,
                            Ocupation,
                            MaritalStatus,
                            IdentityRG,
                            CPF,
                            Street,
                            SpouseFirstName,
                            SpouseLastName,
                            SpouseNationality,
                            SpouseIdentityRG,
                            SpouseCPF
						FROM 
							Renters
						WHERE 
							[AccountId] = @AccountId;";

            var rentersFromDb = _context.Connection.Query<GetRenterQueryResult>(
                    sql,
                    new { AccountId = accountId });

            return rentersFromDb;
        }
    }
}
