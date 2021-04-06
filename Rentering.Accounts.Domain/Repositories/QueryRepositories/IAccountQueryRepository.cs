using Rentering.Accounts.Application.QueryResults;
using System.Collections.Generic;

namespace Rentering.Accounts.Domain.Repositories.QueryRepositories
{
    public interface IAccountQueryRepository
    {
        GetAccountQueryResult GetAccountById(int id);
        IEnumerable<GetAccountQueryResult> GetAccounts();
    }
}
