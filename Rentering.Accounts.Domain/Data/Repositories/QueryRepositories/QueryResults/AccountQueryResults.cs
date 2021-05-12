using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.Accounts.Domain.Data.Repositories.QueryRepositories.QueryResults
{
    public class GetAccountQueryResult
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class GetAccountForLoginQueryResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public e_Roles Role { get; set; }

        public AccountEntity EntityFromQueryResult()
        {
            var email = new EmailValueObject(Email);
            var username = new UsernameValueObject(Username);

            // TODO - Retirar isto
            var passwordFromDb = "123";
            var password = new PasswordValueObject(passwordFromDb);
            var role = Role;
            var id = Id;

            var accountEntity = new AccountEntity(email, username, password, role, id);

            return accountEntity;
        }
    }

    public class GetAccountQueryResult_AdminUsageOnly
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public e_Roles Role { get; set; }
    }
}
