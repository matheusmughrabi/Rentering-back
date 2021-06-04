using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Data.CUDRepositories;
using Rentering.Contracts.Domain.Data.QueryRepositories;
using Rentering.Infra.Contracts.CUDRepositories;
using Rentering.Infra.Contracts.QueryRepositories;

namespace Rentering.Infra.Contracts
{
    public class ContractUnitOfWork : IContractUnitOfWork
    {
        private readonly RenteringDbContext _renteringDbContext;

        public ContractUnitOfWork(RenteringDbContext contractsDbContext)
        {
            _renteringDbContext = contractsDbContext;

            EstateContractCUDRepository = new EstateContractCUDRepository(_renteringDbContext);
            EstateContractQueryRepository = new EstateContractQueryRepository(_renteringDbContext);
            AccountContractCUDRepository = new AccountContractCUDRepository(_renteringDbContext);
        }

        public IEstateContractCUDRepository EstateContractCUDRepository { get; private set; }
        public IEstateContractQueryRepository EstateContractQueryRepository { get; private set; }
        public IAccountContractCUDRepository AccountContractCUDRepository { get; private set; }

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
