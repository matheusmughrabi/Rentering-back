using Microsoft.EntityFrameworkCore;
using Rentering.Corporation.Domain.Data.Repositories;
using Rentering.Corporation.Domain.Entities;
using System.Linq;

namespace Rentering.Infra.Corporations.Repositories
{
    public class CorporationCUDRepository : ICorporationCUDRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public CorporationCUDRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public CorporationEntity Add(CorporationEntity entity)
        {
            if (entity == null)
                return null;

            var addedContractEntity = _renteringDbContext.Corporation.Add(entity).Entity;
            return addedContractEntity;
        }

        public CorporationEntity GetCorporationForCUD(int id)
        {
            var corporationEntity = _renteringDbContext.Corporation
                .Where(c => c.Id == id)
                .Include(c => c.Participants)
                .Include(c => c.MonthlyBalances)
                .ThenInclude(c => c.ParticipantBalances)
                .FirstOrDefault();

            return corporationEntity;
        }
    }
}
