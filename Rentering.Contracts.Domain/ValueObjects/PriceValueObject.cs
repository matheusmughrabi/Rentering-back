using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Contracts.Domain.ValueObjects
{
    public class PriceValueObject : BaseValueObject
    {
        protected PriceValueObject()
        {
        }

        public PriceValueObject(decimal price)
        {
            Price = price;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsGreaterThan(Price, 0M, "Price", "Price must be greater than zero")
            );
        }

        public decimal Price { get; private set; }

        public override string ToString()
        {
            return Price.ToString();
        }
    }
}
