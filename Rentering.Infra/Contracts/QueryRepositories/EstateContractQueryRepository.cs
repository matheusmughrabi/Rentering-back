using Microsoft.EntityFrameworkCore;
using Rentering.Common.Shared.Enums;
using Rentering.Contracts.Domain.Data.QueryRepositories;
using Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Infra.Contracts.QueryRepositories
{
    public class EstateContractQueryRepository : IEstateContractQueryRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public EstateContractQueryRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public GetContractDetailedQueryResult GetContractDetailed(int contractId)
        {
            var contractDetailed = _renteringDbContext.Contract
                .AsNoTracking()
                .Where(c => c.Id == contractId)
                .Include(u => u.Payments.Where(p => p.ContractId == contractId))
                .Include(u => u.Participants.Where(p => p.ContractId == contractId))
                .Select(c => new GetContractDetailedQueryResult() 
                    {
                    Id = c.Id,
                    ContractName = c.ContractName,
                    ContractState = c.ContractState.ToDescriptionString(),
                    RentPrice = c.RentPrice.Price,
                    RentDueDate = c.RentDueDate,
                    ContractStartDate = c.ContractStartDate,
                    ContractEndDate = c.ContractEndDate,

                    Participants = c.Participants
                        .Select(p => new Participant()
                            {
                                AccountId = p.AccountId,
                                FullName = _renteringDbContext.Account
                                    .AsNoTracking()
                                    .Where(u => u.Id == p.AccountId)
                                    .Select(s => s.Name.ToString())
                                    .FirstOrDefault(),
                                Status = p.Status.ToDescriptionString(),
                                ParticipantRole = p.ParticipantRole.ToDescriptionString()
                            })
                            .ToList(),

                     ContractPayments = c.Payments
                        .Select(c => new ContractPayment()
                            {
                                Month = c.Month,
                                RentPrice = c.RentPrice.Price,
                                RenterPaymentStatus = c.RenterPaymentStatus.ToDescriptionString(),
                                TenantPaymentStatus = c.TenantPaymentStatus.ToDescriptionString()
                        })
                            .ToList()
                }).FirstOrDefault();

            return contractDetailed;
        }

        public IEnumerable<GetContractsOfCurrentUserQueryResult> GetContractsOfCurrentUser(int accountId)
        {
            var contractsEntity = _renteringDbContext.Contract
               .AsNoTracking()
               .Where(c => c.Participants.Any(u => u.AccountId == accountId))
               .Include(u => u.Participants.Where(p => p.AccountId == accountId))
               .ToList();

            var contractsQueryResults = new List<GetContractsOfCurrentUserQueryResult>();
            contractsEntity?.ForEach(c => contractsQueryResults.Add(new GetContractsOfCurrentUserQueryResult()
            {
                Id = c.Id,
                ContractName = c.ContractName,
                ContractState = c.ContractState,
                ParticipantRole = c.Participants.FirstOrDefault().ParticipantRole,
                RentPrice = c.RentPrice.Price,
                RentDueDate = c.RentDueDate,
                ContractStartDate = c.ContractStartDate,
                ContractEndDate = c.ContractEndDate
            }));

            return contractsQueryResults;
        }

        public IEnumerable<GetPaymentsOfContractQueryResult> GetPaymentsOfContract(int contractId)
        {
            var paymentsOfContractEntities = _renteringDbContext.ContractPayment
                .AsNoTracking()
                .Where(c => c.ContractId == contractId)
                .ToList();

            var paymentsOfContractQueryResults = new List<GetPaymentsOfContractQueryResult>();
            paymentsOfContractEntities?.ForEach(c => paymentsOfContractQueryResults.Add(new GetPaymentsOfContractQueryResult() 
            {
                ContractId = c.ContractId,
                Month = c.Month,
                RentPrice = c.RentPrice.Price,
                RenterPaymentStatus = c.RenterPaymentStatus,
                TenantPaymentStatus = c.TenantPaymentStatus
            }));

            return paymentsOfContractQueryResults;
        }

        public IEnumerable<GetPendingInvitationsQueryResult> GetPendingInvitations(int accountId)
        {
            var result = _renteringDbContext.AccountContracts
                .AsNoTracking()
                .Where(c => c.AccountId == accountId && c.Status == e_ParticipantStatus.Invited)
                .Include(c => c.EstateContract)
                .Select(p => new GetPendingInvitationsQueryResult() 
                    { 
                        Id = p.ContractId,
                        ContractName = p.EstateContract.ContractName,
                        ContractOwner = "Meg Teste",
                        ContractState = e_ContractState.WaitingParticipantsAccept.ToDescriptionString(),
                        ParticipantRole = e_ParticipantRole.Renter.ToDescriptionString(),
                        RentPrice = 1500M,
                        RentDueDate = DateTime.Now,
                        ContractStartDate = DateTime.Now,
                        ContractEndDate = DateTime.Now.AddYears(1)
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
