using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.Accounts.Domain.Extensions
{
    public static class AccountExtensions
    {
        public static AccountEntity EntityFromQueryResult(this GetAccountForCUDResult accountForCUDResult)
        {
            var email = new EmailValueObject(accountForCUDResult.Email);
            var username = new UsernameValueObject(accountForCUDResult.Username);

            var role = accountForCUDResult.Role;
            var id = accountForCUDResult.Id;

            var accountEntity = new AccountEntity(email, username, null, role, id);

            return accountEntity;
        }

        public static AccountEntity EntityFromQueryResult(this GetAccountForLoginQueryResult accountForLoginQueryResult)
        {
            var email = new EmailValueObject(accountForLoginQueryResult.Email);
            var username = new UsernameValueObject(accountForLoginQueryResult.Username);

            var passwordFromDb = "123";
            var password = new PasswordValueObject(passwordFromDb);
            var role = accountForLoginQueryResult.Role;
            var id = accountForLoginQueryResult.Id;

            var accountEntity = new AccountEntity(email, username, password, role, id);

            return accountEntity;
        }
    }
}
