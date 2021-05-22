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
    }
}
