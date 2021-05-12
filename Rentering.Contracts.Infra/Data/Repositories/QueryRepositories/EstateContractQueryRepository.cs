using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class EstateContractQueryRepository : IEstateContractQueryRepository
    {
        private readonly RenteringDataContext _context;

        public EstateContractQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfContractNameExists(string contractName)
        {
            var sql = @"SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [EstateContracts]
		                        WHERE [ContractName] = @ContractName
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT)
                            END;";

            var contractNameExists = _context.Connection.Query<bool>(
                    sql,
                    new { ContractName = contractName }).FirstOrDefault();

            return contractNameExists;
        }

        public IEnumerable<GetEstateContractQueryResult> GetAll()
        {
            var sql = @"SELECT 
							*
						FROM 
							EstateContracts;";

            var contractsFromDb = _context.Connection.Query<GetEstateContractQueryResult>(sql);

            return contractsFromDb;
        }

        public GetEstateContractQueryResult GetById(int id)
        {
            var sql = @"SELECT
							*
						FROM 
							EstateContracts
						WHERE 
							[Id] = @Id;";

            var contractFromDb = _context.Connection.Query<GetEstateContractQueryResult>(
                    sql,
                    new { Id = id }).FirstOrDefault();

            return contractFromDb;
        }
    }
}
