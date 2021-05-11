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
            var contractNameExists = _context.Connection.Query<bool>(
                    "sp_ContractsWithGuarantor_Query_CheckIfContractNameExists",
                    new { ContractName = contractName },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return contractNameExists;
        }

        public IEnumerable<GetContractWithGuarantorQueryResult> GetAll()
        {
            var contractsFromDb = _context.Connection.Query<GetContractWithGuarantorQueryResult>(
                    "sp_ContractsWithGuarantor_Query_GetAllContracts",
                    commandType: CommandType.StoredProcedure
                );

            return contractsFromDb;
        }

        public GetContractWithGuarantorQueryResult GetById(int id)
        {
            var contractFromDb = _context.Connection.Query<GetContractWithGuarantorQueryResult>(
                    "sp_ContractsWithGuarantor_Query_GetContractById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return contractFromDb;
        }
    }
}
