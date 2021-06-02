using Rentering.Contracts.Domain.DataEF.Repositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.DataEF.Repositories
{
    public interface IEstateContractQueryRepositoryEF
    {
        IEnumerable<GetContractsOfCurrentUserQueryResult> GetContractsOfCurrentUser(int accountId);
        GetContractDetailedQueryResult GetContractDetailed(int contractId);
        IEnumerable<GetPendingInvitationsQueryResult> GetPendingInvitations(int accountId);
    }
}
