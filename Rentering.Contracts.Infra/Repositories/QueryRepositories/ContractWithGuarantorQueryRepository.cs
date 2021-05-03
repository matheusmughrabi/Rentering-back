using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.QueryRepositories
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
    }
}
