using Rentering.Contracts.Domain.DataEF.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Infra.Contracts.CUDRepositories
{
    public class AccountContractCUDRepositoryEF : IAccountContractCUDRepositoryEF
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountContractCUDRepositoryEF(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public AccountContractsEntity GetAccountContractForCUD(int accountId, int contractId)
        {
            var accountContractsEntity = _renteringDbContext.AccountContracts
               .Where(c => c.AccountId == accountId && c.ContractId == contractId)
               .FirstOrDefault();

            return accountContractsEntity;
        }
    }
}
