using Rentering.Accounts.Domain.Entities;
using System.Collections.Generic;

namespace Rentering.Accounts.Domain.Repositories.CUDRepositories
{
    public interface IAccountCUDRepository
    {
        bool CheckIfEmailExists(string email);
        bool CheckIfUsernameExists(string username);
        IEnumerable<AccountEntity> GetAllAccounts();
        AccountEntity GetAccountById(int id);
        void CreateAccount(AccountEntity accountEntity);
        void UpdateAccount(int id, AccountEntity accountEntity);
        void DeleteAccount(int id);
    }
}

