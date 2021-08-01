using Microsoft.EntityFrameworkCore;
using Rentering.Common.Shared.Enums;
using Rentering.Corporation.Domain.Data.Repositories;
using Rentering.Corporation.Domain.Data.Repositories.QueryResults;
using Rentering.Corporation.Domain.Enums;
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

        public GetCorporationDetailedQueryResult GetCorporationDetailed(int currentUserId, int corporationId)
        {
            var result = _renteringDbContext.Corporation
               .AsNoTracking()
               .Include(i => i.Participants)
               .Include(i => i.MonthlyBalances)
               .Where(c => c.Id == corporationId)
               .Select(p => new GetCorporationDetailedQueryResult()
               {
                   Id = p.Id,
                   Name = p.Name,
                   Admin = _renteringDbContext.Account
                               .AsNoTracking()
                               .Where(u => u.Id == p.AdminId)
                               .Select(s => s.Name.ToString())
                               .FirstOrDefault(),
                   IsCurrentUserAdmin = currentUserId == p.AdminId,
                   CreateDate = p.CreateDate,
                   Status = new EnumResult<e_CorporationStatus>() 
                   {
                       Value = p.Status,
                       Description = p.Status.ToDescription()
                   },

                   Participants = p.Participants
                        .Select(u => new Participant()
                        {
                            FullName = _renteringDbContext.Account
                                    .AsNoTracking()
                                    .Where(m => m.Id == u.AccountId)
                                    .Select(s => s.Name.ToString())
                                    .FirstOrDefault(),
                            InvitationStatus = new EnumResult<e_InvitationStatus>()
                            {
                                Value = u.InvitationStatus,
                                Description = u.InvitationStatus.ToDescription()
                            },
                            SharedPercentage = u.SharedPercentage
                        }).ToList(),

                   MonthlyBalances = p.MonthlyBalances
                        .Select(u => new MonthlyBalance()
                        {
                            Id = u.Id,
                            Month = u.Month,
                            TotalProfit = u.TotalProfit,

                            Status = new EnumResult<e_MonthlyBalanceStatus>() 
                            {
                                Value = u.Status,
                                Description = u.Status.ToDescription()
                            },

                            CurrentUserBalanceStatus = u.ParticipantBalances
                                .Where(c => c.Participant.AccountId == currentUserId)
                                .Select(p => new EnumResult<e_ParticipantBalanceStatus>()
                                {
                                    Value = p.Status,
                                    Description = p.Status.ToDescription()
                                })
                                .FirstOrDefault(),

                            ParticipantBalances = u.ParticipantBalances.Select(p => new ParticipantBalance()
                            {
                                ParticipantName = _renteringDbContext.Account
                                    .AsNoTracking()
                                    .Where(m => m.Id == p.Participant.AccountId)
                                    .Select(s => s.Name.ToString())
                                    .FirstOrDefault(),
                                Balance = p.Balance,
                                Status = new EnumResult<e_ParticipantBalanceStatus>() 
                                {
                                    Value = p.Status,
                                    Description = p.Status.ToDescription()
                                },
                            }).ToList()
                        }).ToList()
               })
               .FirstOrDefault();

            return result;
        }

        public IEnumerable<GetInvitationsQueryResult> GetInvitations(int accountId)
        {
            var result = _renteringDbContext.Participant
               .AsNoTracking()
               .Where(c => c.AccountId == accountId && c.InvitationStatus == e_InvitationStatus.Invited && c.Corporation.Status == e_CorporationStatus.WaitingParticipants)
               .Include(c => c.Corporation)
               .Select(p => new GetInvitationsQueryResult()
               {
                   ParticipantId = p.Id,
                   CorporationId = p.CorporationId,
                   Name = p.Corporation.Name,
                   Admin = _renteringDbContext.Account
                        .AsNoTracking()
                        .Where(c => c.Id == p.Corporation.AdminId)
                        .Select(u => u.Name.ToString())
                        .FirstOrDefault()
               })
               .ToList();

            return result;
        }

        public int GetAccountIdByEmail(string email)
        {
            var accountId = _renteringDbContext.Account
                .AsNoTracking()
                .Where(c => c.Email.Email == email)
                .Select(p => p.Id)
                .FirstOrDefault();

            return accountId;
        }
    }
}
