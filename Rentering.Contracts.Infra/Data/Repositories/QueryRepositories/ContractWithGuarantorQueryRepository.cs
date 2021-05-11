using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class ContractWithGuarantorQueryRepository : IContractWithGuarantorQueryRepository
    {
        private readonly RenteringDataContext _context;

        public ContractWithGuarantorQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfContractNameExists(string contractName)
        {
            var sql = @"SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [ContractsWithGuarantor]
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

        public IEnumerable<GetContractWithGuarantorQueryResult> GetAll()
        {
            var sql = @"SELECT 
							*
						FROM 
							ContractsWithGuarantor;";

            var contractsFromDb = _context.Connection.Query<GetContractWithGuarantorQueryResult>(sql);

            return contractsFromDb;
        }

        public GetContractWithGuarantorQueryResult GetById(int id)
        {
            var sql = @"SELECT
							*
						FROM 
							ContractsWithGuarantor
						WHERE 
							[Id] = @Id;";

            var contractFromDb = _context.Connection.Query<GetContractWithGuarantorQueryResult>(
                    sql,
                    new { Id = id }).FirstOrDefault();

            return contractFromDb;
        }
    }
}
