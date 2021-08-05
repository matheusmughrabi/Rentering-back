using Rentering.Corporation.Domain.Data;
using Rentering.Corporation.Domain.Data.Repositories;
using Rentering.Infra.Corporations.Repositories;

namespace Rentering.Infra.Corporations
{
    public class CorporationUnitOfWork : ICorporationUnitOfWork
    {
        private readonly RenteringDbContext _renteringDbContext;

        public CorporationUnitOfWork(RenteringDbContext contractsDbContext)
        {
            _renteringDbContext = contractsDbContext;

            CorporationCUDRepository = new CorporationCUDRepository(_renteringDbContext);
            CorporationQueryRepository = new CorporationQueryRepository(_renteringDbContext);
        }

        public ICorporationCUDRepository CorporationCUDRepository { get; private set; }
        public ICorporationQueryRepository CorporationQueryRepository { get; private set; }

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
