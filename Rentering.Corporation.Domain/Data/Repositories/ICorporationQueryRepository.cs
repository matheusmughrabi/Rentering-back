using Rentering.Common.Shared.QueryResults;
using Rentering.Corporation.Domain.Data.Repositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Data.Repositories
{
    public interface ICorporationQueryRepository
    {
        PaginatedQueryResult<GetCorporationsQueryResult> GetCorporations(int accountId, int page = 1, int recordsPerPage = 10);
        SingleQueryResult<GetCorporationDetailedQueryResult> GetCorporationDetailed(int accountId, int corporationId);
        IEnumerable<GetInvitationsQueryResult> GetInvitations(int accountId);
        int GetAccountIdByEmail(string email);
    }
}
