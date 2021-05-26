using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Contracts.Domain.ValueObjects
{
    public class NameValueObject : BaseValueObject
    {
        protected NameValueObject()
        {
        }

        public NameValueObject(
            string firstName, 
            string lastName, 
            bool firstNameRequired = true, 
            bool lastNameRequired = true)
        {
            FirstName = firstName;
            LastName = lastName;

            Validate(firstNameRequired, lastNameRequired);
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        private void Validate(bool firstNameRequired, bool lastNameRequired)
        {
            if (firstNameRequired)
            {
                AddNotifications(new ValidationContract()
                .Requires()
                .IsNotNull(FirstName, "FirstName", "FirstName cannot be null or empty")
                );
            }

            if (lastNameRequired)
            {
                AddNotifications(new ValidationContract()
                .Requires()
                .IsNotNull(LastName, "LastName", "LastName cannot be null or empty ")
                );
            }

            if (FirstName != null)
            {
                AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "First Name must have at least 3 letters")
                .HasMaxLen(FirstName, 40, "FirstName", "First Name must have less than 40 letters")
                );
            }

            if (LastName != null)
            {
                AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(LastName, 3, "LastName", "Last Name must have at least 3 letters")
                .HasMaxLen(LastName, 40, "LastName", "Last Name must have less than 40 letters")
                );
            }
        }
    }
}
