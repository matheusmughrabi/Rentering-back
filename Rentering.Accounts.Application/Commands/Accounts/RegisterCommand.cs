using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Commands.Accounts
{
    public class RegisterCommand : Command
    {
        public RegisterCommand(string firstName, string lastName, string email, string username, string password, string confirmPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;

            FailFastValidations();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public override void FailFastValidations()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(FirstName, 3, "Primeiro nome", "O primeiro nome deve conter no mínimo uma letra")
                .HasMinLen(LastName, 3, "Sobrenome", "O sobrenome deve conter no mínimo uma letra")
                .IsEmail(Email, "Email", "Email inválido")
                .HasMinLen(Username, 3, "Usuário", "O nome de usuário precisa ter ao menos 3 letras")
                .HasMaxLen(Username, 40, "Usuário", "O nome de usuário precisa ter menos do que 40 letras")
                .HasMinLen(Password, 3, "Senha", "A senha precisa ter ao menos 3 letras")
                .HasMaxLen(Password, 40, "Senha", "A senha precisa ter menos do que 40 letras")
                .IsTrue(Password == ConfirmPassword, "Senha", "As senhas não conferem")
            );
        }
    }
}
