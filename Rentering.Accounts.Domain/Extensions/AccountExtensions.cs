using Rentering.Accounts.Application.QueryResults;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.Accounts.Domain.Extensions
{
    public static class AccountExtensions
    {
        public static AccountEntity EntityFromQueryResult(this GetAccountQueryResult accountQueryResult)
        {
            var email = new EmailValueObject(accountQueryResult.Email);
            var username = new UsernameValueObject(accountQueryResult.Username);
            var password = new PasswordValueObject(accountQueryResult.Password);
            var role = accountQueryResult.Role;
            var id = accountQueryResult.Id;

            var accountEntity = new AccountEntity(email, username, password, role, id);

            return accountEntity;
        }
    }
}
