using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Commands
{
    public class LoginAccountCommand : Command
    {
        public LoginAccountCommand(string username, string password)
        {
            Username = username;
            Password = password;

            FailFastValidations();
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public override void FailFastValidations()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Username, 3, "Usuário", "O nome de usuário precisa ter ao menos 3 letras")
                .HasMaxLen(Username, 40, "Usuário", "O nome de usuário precisa ter menos do que 40 letras")
                .HasMinLen(Password, 3, "Senha", "A senha precisa ter ao menos 3 letras")
                .HasMaxLen(Password, 40, "Senha", "A senha precisa ter menos do que 40 letras")
            );
        }
    }
}
