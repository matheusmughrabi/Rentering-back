using Rentering.Accounts.Domain.Entities;

namespace Rentering.Accounts.Domain.Data.RespositoriesEF
{
    public interface IAccountCUDRepositoryEF
    {
        AccountEntity GetAccountForCUD(int accountId);
        AccountEntity GetAccountForLogin(string username);
        bool EmailExists(string email);
        bool UsernameExists(string username);
        AccountEntity Add(AccountEntity accountEntity);
        AccountEntity Delete(AccountEntity accountEntity);
        AccountEntity Delete(int id);
    }
}
