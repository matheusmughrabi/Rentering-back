using Rentering.Common.Shared.QueryResults;
using Rentering.Corporation.Domain.Data.Repositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Data.Repositories
{
    public interface ICorporationQueryRepository
    {
        ListQueryResult<GetCorporationsQueryResult> GetCorporations(int accountId);
        SingleQueryResult<GetCorporationDetailedQueryResult> GetCorporationDetailed(int accountId, int corporationId);
        IEnumerable<GetInvitationsQueryResult> GetInvitations(int accountId);
        int GetAccountIdByEmail(string email);
    }
}
