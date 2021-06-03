using Rentering.Contracts.Domain.DataEF.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.DataEF.QueryRepositories
{
    public interface IEstateContractQueryRepositoryEF
    {
        IEnumerable<GetContractsOfCurrentUserQueryResult> GetContractsOfCurrentUser(int accountId);
        GetContractDetailedQueryResult GetContractDetailed(int contractId);
        IEnumerable<GetPendingInvitationsQueryResult> GetPendingInvitations(int accountId);
        IEnumerable<GetPaymentsOfContractQueryResult> GetPaymentsOfContract(int contractId);
    }
}
