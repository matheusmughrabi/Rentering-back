using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractEntity : BaseEntity
    {
        public ContractEntity(
            string contractName,
            PriceValueObject rentPrice,
            int renterId,
            int tentantId)
        {
            ContractName = contractName;
            RentPrice = rentPrice;
            RenterId = renterId;
            TentantId = tentantId;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(ContractName, 3, "ContractName", "Contract name must have at least 3 letters")
                .HasMaxLen(ContractName, 40, "ContractName", "Contract name must have less than 40 letters")
                .IsTrue(RenterTenantIdsValidation(), "RenterId/TentantId", "RenterId and TenantId cannot be equal")
            );
        }

        public string ContractName { get; private set; }
        public PriceValueObject RentPrice { get; private set; }
        public int RenterId { get; private set; }
        public int TentantId { get; private set; }

        public void UpdateRentPrice(PriceValueObject rentPrice)
        {
            if (rentPrice.Price < 0)
            {
                AddNotifications(rentPrice.Notifications);
                return;
            }

            RentPrice = rentPrice;
        }

        private bool RenterTenantIdsValidation()
        {
            if (RenterId == TentantId)
                return false;

            return true;
        }
    }
}
