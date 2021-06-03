using Rentering.Contracts.Domain.DataEF;
using Rentering.Contracts.Domain.DataEF.CUDRepositories;
using Rentering.Contracts.Domain.DataEF.QueryRepositories;
using Rentering.Infra.Contracts.CUDRepositories;
using Rentering.Infra.Contracts.QueryRepositories;

namespace Rentering.Infra.Contracts
{
    public class ContractUnitOfWorkEF : IContractUnitOfWorkEF
    {
        private readonly RenteringDbContext _renteringDbContext;

        public ContractUnitOfWorkEF(RenteringDbContext contractsDbContext)
        {
            _renteringDbContext = contractsDbContext;

            EstateContractCUDRepositoryEF = new EstateContractCUDRepositoryEF(_renteringDbContext);
            EstateContractQueryRepositoryEF = new EstateContractQueryRepositoryEF(_renteringDbContext);
            AccountContractCUDRepositoryEF = new AccountContractCUDRepositoryEF(_renteringDbContext);
        }

        public IEstateContractCUDRepositoryEF EstateContractCUDRepositoryEF { get; private set; }
        public IEstateContractQueryRepositoryEF EstateContractQueryRepositoryEF { get; private set; }
        public IAccountContractCUDRepositoryEF AccountContractCUDRepositoryEF { get; private set; }

        public void Dispose()
        {
            _renteringDbContext?.Dispose();
        }

        public void Save()
        {
            _renteringDbContext.SaveChanges();
        }
    }
}
