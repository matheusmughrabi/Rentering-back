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

        public IEnumerable<GetRenterQueryResult> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public GetRenterQueryResult GetById(int id)
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
    }
}
