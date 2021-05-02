using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.UtilRepositories;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.UtilRepositories
{
    public class ContractWithGuarantorUtilRepository : IContractWithGuarantorUtilRepository
    {
        private readonly RenteringDataContext _context;

        public ContractWithGuarantorUtilRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfContractNameExists(string contractName)
        {
            var accountExists = _context.Connection.Query<bool>(
                    "sp_ContractsWithGuarantor_Util_CheckIfContractNameExists",
                    new { ContractName = contractName },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return accountExists;
        }
    }
}
