using Rentering.Accounts.Application.QueryResults;
using System.Collections.Generic;

namespace Rentering.Accounts.Domain.Repositories.QueryRepositories
{
    public interface IAccountQueryRepository
    {
        bool CheckIfEmailExists(string email);
        bool CheckIfUsernameExists(string username);
        GetAccountQueryResult GetAccountById(int id);
        IEnumerable<GetAccountQueryResult> GetAccounts();
    }
}
