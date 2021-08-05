using Microsoft.EntityFrameworkCore;
using Rentering.Common.Shared.Enums;
using Rentering.Contracts.Domain.Data.Repositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryResults;
using Rentering.Contracts.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Infra.Contracts.Repositories
{
    public class ContractQueryRepository : IContractQueryRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public ContractQueryRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public GetContractDetailedQueryResult GetContractDetailed(int accountId, int contractId)
        {
            var contractDetailed = _renteringDbContext.Contract
                .AsNoTracking()
                .Where(c => c.Id == contractId)
                .Include(u => u.Payments.Where(p => p.ContractId == contractId))
                .Include(u => u.Participants.Where(p => p.ContractId == contractId))
                .Select(c => new GetContractDetailedQueryResult()
                {
                    Id = c.Id,
                    CurrentUserRole = c.Participants.Where(c => c.AccountId == accountId)
                        .Select(p => new EnumResult<e_ParticipantRole>() 
                        {
                            Value = p.ParticipantRole,
                            Description = p.ParticipantRole.ToDescription()
                        })
                        .FirstOrDefault(),
                    ContractName = c.ContractName,
                    ContractState = new EnumResult<e_ContractState>() 
                    { 
                        Value = c.ContractState,
                        Description = c.ContractState.ToDescription()
                    },
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
                            Status = new EnumResult<e_ParticipantStatus>() 
                            {
                                Value = p.Status,
                                Description = p.Status.ToDescription()
                            },
                            ParticipantRole = new EnumResult<e_ParticipantRole>() 
                            {
                                Value = p.ParticipantRole,
                                Description = p.ParticipantRole.ToDescription()
                            },
                        })
                            .ToList(),

                    ContractPayments = c.Payments
                        .Select(c => new ContractPayment()
                        {
                            Month = c.Month,
                            RentPrice = c.RentPrice.Price,
                            ReceiverPaymentStatus = new EnumResult<e_ReceiverPaymentStatus>() 
                            {
                                Value = c.ReceiverPaymentStatus,
                                Description = c.ReceiverPaymentStatus.ToDescription()
                            },
                            PayerPaymentStatus = new EnumResult<e_PayerPaymentStatus>() 
                            { 
                                Value = c.PayerPaymentStatus,
                                Description = c.PayerPaymentStatus.ToDescription()
                            }
                        }).ToList()
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
                ContractState = new EnumResult<e_ContractState>() 
                {
                    Value = c.ContractState,
                    Description = c.ContractState.ToDescription()
                },
                ParticipantRole = new EnumResult<e_ParticipantRole>() 
                {
                    Value = c.Participants.FirstOrDefault().ParticipantRole,
                    Description = c.Participants.FirstOrDefault().ParticipantRole.ToDescription()
                }, 
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
                ReceiverPaymentStatus = new EnumResult<e_ReceiverPaymentStatus>() 
                {
                    Value = c.ReceiverPaymentStatus,
                    Description = c.ReceiverPaymentStatus.ToDescription()
                },
                PayerPaymentStatus = new EnumResult<e_PayerPaymentStatus>() 
                {
                    Value = c.PayerPaymentStatus,
                    Description = c.PayerPaymentStatus.ToDescription()
                },
            }));

            return paymentsOfContractQueryResults;
        }

        public IEnumerable<GetPendingInvitationsQueryResult> GetPendingInvitations(int accountId)
        {
            var result = _renteringDbContext.AccountContracts
                .AsNoTracking()
                .Where(c => c.AccountId == accountId && c.Status == e_ParticipantStatus.Pending)
                .Include(c => c.Contract)
                .Select(p => new GetPendingInvitationsQueryResult()
                {
                    AccountContractId = p.Id,
                    ContractId = p.ContractId,
                    ContractName = p.Contract.ContractName,
                    ContractOwner = "Matheus Campanini Mughrabi Mockado",
                    ContractState = new EnumResult<e_ContractState>() 
                    {
                        Value = p.Contract.ContractState,
                        Description = p.Contract.ContractState.ToDescription()
                    },
                    ParticipantRole = p.ParticipantRole.ToDescription(),
                    RentPrice = p.Contract.RentPrice.Price,
                    RentDueDate = p.Contract.RentDueDate,
                    ContractStartDate = p.Contract.ContractStartDate,
                    ContractEndDate = p.Contract.ContractEndDate
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
