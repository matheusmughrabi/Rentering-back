using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Accounts.Domain.ValueObjects
{
    public class PersonNameValueObject : BaseValueObject
    {
        private PersonNameValueObject()
        {
        }

        public PersonNameValueObject(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "O primeiro nome deve conter no mínimo uma letra")
                .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter no mínimo uma letra")
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
