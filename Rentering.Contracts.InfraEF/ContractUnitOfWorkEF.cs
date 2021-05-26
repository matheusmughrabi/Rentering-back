using Rentering.Contracts.Domain.DataEF;
using Rentering.Contracts.Domain.DataEF.Repositories;
using Rentering.Contracts.InfraEF.Repositories;

namespace Rentering.Contracts.InfraEF
{
    public class ContractUnitOfWorkEF : IContractUnitOfWorkEF
    {
        private readonly ContractsDbContext _contractsDbContext;

        public ContractUnitOfWorkEF(ContractsDbContext contractsDbContext)
        {
            _contractsDbContext = contractsDbContext;

            EstateContractCUDRepositoryEF = new EstateContractCUDRepositoryEF(_contractsDbContext);
        }

        public IEstateContractCUDRepositoryEF EstateContractCUDRepositoryEF { get; private set; }

        public void Dispose()
        {
            _contractsDbContext?.Dispose();
        }

        public void Save()
        {
            _contractsDbContext.SaveChanges();
        }
    }
}
