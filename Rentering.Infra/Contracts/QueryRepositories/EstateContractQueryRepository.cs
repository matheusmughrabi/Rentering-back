using Microsoft.EntityFrameworkCore;
using Rentering.Contracts.Domain.Data.QueryRepositories;
using Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Enums;
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
            var contractEntity = _renteringDbContext.Contract
                .AsNoTracking()
                .Where(c => c.Id == contractId)
                .Include(u => u.Participants.Where(p => p.ContractId == contractId))
                .Include(u => u.Payments.Where(p => p.ContractId == contractId))
                .FirstOrDefault();

            if (contractEntity == null)
                return null;

            var contractQueryResult = new GetContractDetailedQueryResult()
            {
                Id = contractEntity.Id,
                ContractName = contractEntity.ContractName,
                RentPrice = contractEntity.RentPrice.Price,
                RentDueDate = contractEntity.RentDueDate,
                ContractStartDate = contractEntity.ContractStartDate,
                ContractEndDate = contractEntity.ContractEndDate,

                Participants = contractEntity.Participants
                    .Select(c => new Participant() 
                    { 
                        AccountId = c.AccountId, 
                        Status = c.Status, 
                        ParticipantRole = c.ParticipantRole
                    })
                    .ToList(),

                ContractPayments = contractEntity.Payments
                    .Select(c => new ContractPayment() 
                    { 
                        Month = c.Month, 
                        RentPrice = c.RentPrice.Price, 
                        RenterPaymentStatus = c.RenterPaymentStatus, 
                        TenantPaymentStatus = c.TenantPaymentStatus 
                    })
                    .ToList(),
            };

            return contractQueryResult;
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
            var accountContractsEntitiesPending = _renteringDbContext.AccountContracts
                .AsNoTracking()
                .Where(c => c.AccountId == accountId && c.Status == e_ParticipantStatus.Invited)
                .Include(c => c.EstateContract)
                .ToList();

            var contractsQueryResults = new List<GetPendingInvitationsQueryResult>();
            accountContractsEntitiesPending?.ForEach(c => contractsQueryResults.Add(new GetPendingInvitationsQueryResult()
            {
                ContractName = c.EstateContract.ContractName
            }));

            return contractsQueryResults;
        }
    }
}
