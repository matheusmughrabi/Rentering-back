using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Accounts.Domain.Data.Repositories.QueryRepositories
{
    public interface IAccountQueryRepository
    {
        bool CheckIfEmailExists(string email);
        bool CheckIfUsernameExists(string username);
        GetAccountQueryResult GetAccountById(int id);
        IEnumerable<GetAccountQueryResult_AdminUsageOnly> GetAllAccounts_AdminUsageOnly();
    }
}
