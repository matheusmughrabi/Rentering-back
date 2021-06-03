using Rentering.Accounts.Domain.Data.RespositoriesEF.QueryResults;
using System.Collections.Generic;

namespace Rentering.Accounts.Domain.Data.RespositoriesEF
{
    public interface IAccountQueryRepositoryEF
    {
        GetAccountQueryResultEF GetAccountById(int id);
        IEnumerable<GetAccountQueryResult_AdminUsageOnlyEF> GetAllAccounts_AdminUsageOnly();
    }
}
