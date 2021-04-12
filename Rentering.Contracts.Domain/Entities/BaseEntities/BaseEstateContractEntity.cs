using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Entities
{
    public abstract class BaseEstateContractEntity : Entity
    {
        protected BaseEstateContractEntity(string contractName, PriceValueObject rentPrice)
        {
            ContractName = contractName;
            RentPrice = rentPrice;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(ContractName, 3, "ContractName", "Contract name must have at least 3 letters")
                .HasMaxLen(ContractName, 40, "ContractName", "Contract name must have less than 40 letters")
            );
        }

        public string ContractName { get; private set; }
        public PriceValueObject RentPrice { get; private set; }

        public void UpdateRentPrice(PriceValueObject rentPrice)
        {
            if (rentPrice.Price < 0)
            {
                AddNotifications(rentPrice.Notifications);
                return;
            }

            RentPrice = rentPrice;
        }
    }
}
