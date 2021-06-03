using Microsoft.EntityFrameworkCore;
using Rentering.Contracts.Domain.DataEF.Repositories;
using Rentering.Contracts.Domain.DataEF.Repositories.QueryResults;
using Rentering.Contracts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.InfraEF.Repositories
{
    public class EstateContractQueryRepositoryEF : IEstateContractQueryRepositoryEF
    {
        private readonly ContractsDbContext _contractsDbContext;

        public EstateContractQueryRepositoryEF(ContractsDbContext contractsDbContext)
        {
            _contractsDbContext = contractsDbContext;
        }

        public GetContractDetailedQueryResult GetContractDetailed(int contractId)
        {
            var contractEntity = _contractsDbContext.Contract
                .AsNoTracking()
                .Where(c => c.Id == contractId)
                .FirstOrDefault();

            if (contractEntity == null)
                return null;

            var contractQueryResult = new GetContractDetailedQueryResult()
            {
                Id = contractEntity.Id,
                ContractName = contractEntity.ContractName,
                Street = contractEntity.Address.Street,
                Neighborhood = contractEntity.Address.Neighborhood,
                City = contractEntity.Address.City,
                CEP = contractEntity.Address.CEP,
                State = contractEntity.Address.State,
                PropertyRegistrationNumber = contractEntity.PropertyRegistrationNumber.Number,
                RentPrice = contractEntity.RentPrice.Price,
                RentDueDate = contractEntity.RentDueDate,
                ContractStartDate = contractEntity.ContractStartDate,
                ContractEndDate = contractEntity.ContractEndDate
            };

            return contractQueryResult;
        }

        public IEnumerable<GetContractsOfCurrentUserQueryResult> GetContractsOfCurrentUser(int accountId)
        {
            var contractsEntity = _contractsDbContext.Contract
               .AsNoTracking()
               .Where(c => c.Participants.Any(u => u.AccountId == accountId))
               .ToList();

            var contractsQueryResults = new List<GetContractsOfCurrentUserQueryResult>();
            contractsEntity?.ForEach(c => contractsQueryResults.Add(new GetContractsOfCurrentUserQueryResult()
            {
                Id = c.Id,
                Name = c.ContractName,
                RentPrice = c.RentPrice.Price,
                ContractStartDate = c.ContractStartDate,
                ContractEndDate = c.ContractEndDate
            }));

            return contractsQueryResults;
        }

        public IEnumerable<GetPendingInvitationsQueryResult> GetPendingInvitations(int accountId)
        {
            var accountContractsEntitiesPending = _contractsDbContext.AccountContracts
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
