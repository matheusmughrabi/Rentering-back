using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class RenterCUDRepository : IRenterCUDRepository
    {
        private readonly RenteringDataContext _context;

        public RenterCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfAccountExists(int accountId)
        {
            throw new NotImplementedException();
        }

        public void CreateRenter(RenterEntity renter)
        {
            throw new NotImplementedException();
        }
    }
}
