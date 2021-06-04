using Rentering.Contracts.Domain.Data.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Infra.Contracts.CUDRepositories
{
    public class AccountContractCUDRepository : IAccountContractCUDRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountContractCUDRepository(RenteringDbContext renteringDbContext)
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
