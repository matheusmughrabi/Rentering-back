using FluentValidator;

namespace Rentering.Contracts.Domain.ValueObjects
{
    public class PropertyRegistrationNumberValueObject : Notifiable
    {
        public PropertyRegistrationNumberValueObject(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
