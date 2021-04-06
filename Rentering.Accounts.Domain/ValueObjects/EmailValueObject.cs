using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Accounts.Domain.ValueObjects
{
    public class EmailValueObject : BaseValueObject
    {
        public EmailValueObject(string emailAddress)
        {
            Email = emailAddress;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsEmail(Email, "Email", "Invalid Email Address")
            );
        }

        public string Email { get; private set; }

        public override string ToString()
        {
            return Email;
        }
    }
}
