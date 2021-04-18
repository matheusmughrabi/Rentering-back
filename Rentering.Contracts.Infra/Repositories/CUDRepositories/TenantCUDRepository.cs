using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class TenantCUDRepository : ITenantCUDRepository
    {
        private readonly RenteringDataContext _context;

        public TenantCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfAccountExists(int accountId)
        {
            throw new NotImplementedException();
        }

        public void CreateTenant(TenantEntity tenant)
        {
            throw new NotImplementedException();
        }

        public void UpdateTenant(int id, TenantEntity tenant)
        {
            throw new NotImplementedException();
        }

        public void DeleteTenant(int id)
        {
            throw new NotImplementedException();
        }
    }
}
