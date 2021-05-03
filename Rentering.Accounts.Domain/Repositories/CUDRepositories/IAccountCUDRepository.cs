using Rentering.Accounts.Domain.Entities;

namespace Rentering.Accounts.Domain.Repositories.CUDRepositories
{
    public interface IAccountCUDRepository
    {
        void CreateAccount(AccountEntity accountEntity);
        void UpdateAccount(int id, AccountEntity accountEntity);
        void DeleteAccount(int id);
    }
}

