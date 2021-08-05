using Rentering.Accounts.Domain.Data.Repositories.QueryResults;

namespace Rentering.Accounts.Domain.Data.Repositories
{
    public interface IAccountQueryRepository
    {
        GetAccountQueryResult GetAccountById(int id);
    }
}
