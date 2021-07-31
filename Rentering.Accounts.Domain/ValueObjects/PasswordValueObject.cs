using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Accounts.Domain.ValueObjects
{
    public class PasswordValueObject : BaseValueObject
    {
        private readonly string _confirmPassword;

        private PasswordValueObject()
        {
        }

        public PasswordValueObject(string password)
        {
            Password = password;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Password, 3, "Senha", "A senha precisa ter ao menos 3 letras")
                .HasMaxLen(Password, 40, "Senha", "A senha precisa ter menos do que 40 letras")
            );
        }

        public PasswordValueObject(string password, string confirmPassword)
        {
            Password = password;
            _confirmPassword = confirmPassword;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Password, 3, "Senha", "A senha precisa ter ao menos 3 letras")
                .HasMaxLen(Password, 40, "Senha", "A senha precisa ter menos do que 40 letras")
                .IsTrue(CheckIfPasswordsMatch(), "Senha", "As senhas não conferem")
            );
        }

        public string Password { get; private set; }

        private bool CheckIfPasswordsMatch()
        {
            if (Password == _confirmPassword)
                return true;

            return false;
        }

        public override string ToString()
        {
            return Password;
        }
    }
}
