using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Accounts.Domain.ValueObjects
{
    public class NameValueObject : BaseValueObject
    {
        public NameValueObject(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "First Name must have at least 3 letters")
                .HasMaxLen(FirstName, 40, "FirstName", "First Name must have less than 40 letters")
                .HasMinLen(LastName, 3, "LastName", "Last Name must have at least 3 letters")
                .HasMaxLen(LastName, 40, "LastName", "Last Name must have less than 40 letters")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
