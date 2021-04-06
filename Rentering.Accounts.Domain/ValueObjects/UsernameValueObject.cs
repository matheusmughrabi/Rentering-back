using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Accounts.Domain.ValueObjects
{
    public class UsernameValueObject : BaseValueObject
    {
        public UsernameValueObject(string username)
        {
            Username = username;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Username, 3, "Username", "Username must have at least 3 letters")
                .HasMaxLen(Username, 40, "Username", "Username must have less than 40 letters")
            );
        }

        public string Username { get; private set; }

        public override string ToString()
        {
            return Username;
        }
    }
}
