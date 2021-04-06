using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Accounts.Domain.ValueObjects
{
    public class PasswordValueObject : BaseValueObject
    {
        private readonly string _confirmPassword;

        public PasswordValueObject(string password)
        {
            Password = password;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Password, 3, "Password", "Password must have at least 3 letters")
                .HasMaxLen(Password, 40, "Password", "Password must have less than 40 letters")
            );
        }

        public PasswordValueObject(string password, string confirmPassword)
        {
            Password = password;
            _confirmPassword = confirmPassword;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Password, 3, "Password", "Password must have at least 3 letters")
                .HasMaxLen(Password, 40, "Password", "Password must have less than 40 letters")
                .IsTrue(CheckIfPasswordsMatch(), "Password", "Passwords don't match")
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
