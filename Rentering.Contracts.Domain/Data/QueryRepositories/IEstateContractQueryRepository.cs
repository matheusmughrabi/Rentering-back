using Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.QueryRepositories
{
    public interface IEstateContractQueryRepository
    {
        IEnumerable<GetContractsOfCurrentUserQueryResult> GetContractsOfCurrentUser(int accountId);
        GetContractDetailedQueryResult GetContractDetailed(int contractId);
        IEnumerable<GetPendingInvitationsQueryResult> GetPendingInvitations(int accountId);
        IEnumerable<GetPaymentsOfContractQueryResult> GetPaymentsOfContract(int contractId);
        int GetAccountIdByEmail(string email);
    }
}
