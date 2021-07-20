using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Data.Repositories;
using Rentering.Infra.Contracts.Repositories;

namespace Rentering.Infra.Contracts
{
    public class ContractUnitOfWork : IContractUnitOfWork
    {
        private readonly RenteringDbContext _renteringDbContext;

        public ContractUnitOfWork(RenteringDbContext contractsDbContext)
        {
            _renteringDbContext = contractsDbContext;

            ContractCUDRepository = new ContractCUDRepository(_renteringDbContext);
            ContractQueryRepository = new ContractQueryRepository(_renteringDbContext);
        }

        public IContractCUDRepository ContractCUDRepository { get; private set; }
        public IContractQueryRepository ContractQueryRepository { get; private set; }

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
