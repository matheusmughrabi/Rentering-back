using Rentering.Contracts.Domain.Data.Repositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.Repositories
{
    public interface IContractQueryRepository
    {
        IEnumerable<GetContractsOfCurrentUserQueryResult> GetContractsOfCurrentUser(int accountId);
        GetContractDetailedQueryResult GetContractDetailed(int accountId, int contractId);
        IEnumerable<GetPendingInvitationsQueryResult> GetPendingInvitations(int accountId);
        IEnumerable<GetPaymentsOfContractQueryResult> GetPaymentsOfContract(int contractId);
        int GetAccountIdByEmail(string email);
    }
}
