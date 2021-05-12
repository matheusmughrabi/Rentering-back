using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.Accounts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults
{
    public class GetAccountForCUDResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public e_Roles Role { get; set; }

        public AccountEntity EntityFromModel()
        {
            var email = new EmailValueObject(Email);
            var username = new UsernameValueObject(Username);
            var role = Role;
            var id = Id;

            var accountEntity = new AccountEntity(email, username, null, role, id);

            return accountEntity;
        }
    }

    public class GetAccountForLoginCUDResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public e_Roles Role { get; set; }

        public AccountEntity EntityFromModel()
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
}
