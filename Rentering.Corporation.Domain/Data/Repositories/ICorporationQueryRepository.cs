using Rentering.Common.Shared.Data.QueryResults;
using Rentering.Corporation.Domain.Data.Repositories.QueryResults;

namespace Rentering.Corporation.Domain.Data.Repositories
{
    public interface ICorporationQueryRepository
    {
        ListQueryResult<GetCorporationsQueryResult> GetCorporations(int accountId);
        SingleQueryResult<GetCorporationDetailedQueryResult> GetCorporationDetailed(int accountId, int corporationId);
        ListQueryResult<GetInvitationsQueryResult> GetInvitations(int accountId);
        SingleQueryResult<GetPeriodDetailedQueryResult> GetPeriodDetailed(int monthlyBalanceId);
        int GetAccountIdByEmail(string email);
    }
}
