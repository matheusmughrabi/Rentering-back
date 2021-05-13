using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
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

        public IEnumerable<GetTenantQueryResult> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public GetTenantQueryResult GetById(int id)
        {
            var sql = @"SELECT 
							Id,
                            ContractId,
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
                            SpouseOcupation,
                            SpouseIdentityRG,
                            SpouseCPF
						FROM 
							Tenants
						WHERE 
							[Id] = @Id;";

            var renterFromDb = _context.Connection.Query<GetTenantQueryResult>(
                   sql,
                   new { Id = id }).FirstOrDefault();

            return renterFromDb;
        }

        // TODO - Remover
        public IEnumerable<GetTenantQueryResult> GetTenantProfilesOfCurrentUser(int accountId)
        {
            var sql = @"SELECT 
							Id,
                            ContractId,
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
                            SpouseOcupation,
                            SpouseIdentityRG,
                            SpouseCPF
						FROM 
							Tenants
						WHERE 
							[AccountId] = @AccountId;";

            var rentersFromDb = _context.Connection.Query<GetTenantQueryResult>(
                    sql,
                    new { AccountId = accountId });

            return rentersFromDb;
        }
    }
}
