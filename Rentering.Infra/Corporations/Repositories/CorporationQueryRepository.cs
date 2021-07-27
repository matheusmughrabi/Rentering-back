using Microsoft.EntityFrameworkCore;
using Rentering.Common.Shared.Enums;
using Rentering.Corporation.Domain.Data.Repositories;
using Rentering.Corporation.Domain.Data.Repositories.QueryResults;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Infra.Corporations.Repositories
{
    public class CorporationQueryRepository : ICorporationQueryRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public CorporationQueryRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public IEnumerable<GetCorporationsQueryResult> GetCorporations(int accountId)
        {
            var result = _renteringDbContext.Corporation
                .AsNoTracking()
                .Where(c => c.Participants.Any(u => u.AccountId == accountId) || c.AdminId == accountId)
                .Select(p => new GetCorporationsQueryResult()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Admin = _renteringDbContext.Account
                                .AsNoTracking()
                                .Where(u => u.Id == accountId)
                                .Select(s => s.Name.ToString())
                                .FirstOrDefault()
                })
                .ToList();

            return result;
        }

        public GetCorporationDetailedQueryResult GetCorporationDetailed(int accountId, int corporationId)
        {
            var result = _renteringDbContext.Corporation
               .AsNoTracking()
               .Include(i => i.Participants)
               .Include(i => i.MonthlyBalances)
               .Where(c => c.Participants.Any(u => u.AccountId == accountId) || c.AdminId == accountId)
               .Select(p => new GetCorporationDetailedQueryResult()
               {
                   Id = p.Id,
                   Name = p.Name,
                   Admin = _renteringDbContext.Account
                               .AsNoTracking()
                               .Where(u => u.Id == accountId)
                               .Select(s => s.Name.ToString())
                               .FirstOrDefault(),
                   CreateDate = p.CreateDate,

                   Participants = p.Participants
                        .Select(u => new Participant()
                        {
                            FullName = _renteringDbContext.Account
                                    .AsNoTracking()
                                    .Where(m => m.Id == u.AccountId)
                                    .Select(s => s.Name.ToString())
                                    .FirstOrDefault(),
                            InvitationStatus = u.InvitationStatus.ToDescriptionString(),
                            SharedPercentage = u.SharedPercentage
                        }).ToList(),

                   MonthlyBalances = p.MonthlyBalances
                        .Select(u => new MonthlyBalance()
                        {
                            Month = u.Month,
                            TotalProfit = u.TotalProfit
                        }).ToList()
               })
               .FirstOrDefault();

            return result;
        }
    }
}
