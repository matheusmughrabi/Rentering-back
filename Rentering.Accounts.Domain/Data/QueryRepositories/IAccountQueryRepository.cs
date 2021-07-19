using Rentering.Accounts.Domain.Data.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Accounts.Domain.Data.QueryRepositories
{
    public interface IAccountQueryRepository
    {
        GetAccountQueryResultEF GetAccountById(int id);
    }
}
