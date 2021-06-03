using Rentering.Contracts.Domain.DataEF.Repositories;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Contracts.InfraEF.Repositories
{
    public class AccountContractCUDRepositoryEF : IAccountContractCUDRepositoryEF
    {
        private readonly ContractsDbContext _contractsDbContext;

        public AccountContractCUDRepositoryEF(ContractsDbContext contractsDbContext)
        {
            _contractsDbContext = contractsDbContext;
        }

        public AccountContractsEntity GetAccountContractForCUD(int accountId, int contractId)
        {
            var accountContractsEntity = _contractsDbContext.AccountContracts
               .Where(c => c.AccountId == accountId && c.ContractId == contractId)
               .FirstOrDefault();

            return accountContractsEntity;
        }
    }
}
