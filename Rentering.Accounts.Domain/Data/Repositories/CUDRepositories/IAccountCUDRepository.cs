using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Accounts.Domain.Entities;
using Rentering.Common.Shared.Data.Repositories;

namespace Rentering.Accounts.Domain.Data.Repositories.CUDRepositories
{
    public interface IAccountCUDRepository : IGenericCUDRepository<AccountEntity>
    {
        GetAccountForCUDResult GetAccountForCUD(int id);
    }
}

