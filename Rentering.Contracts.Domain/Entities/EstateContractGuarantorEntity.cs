using FluentValidator.Validation;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Entities
{
    public class EstateContractGuarantorEntity : BaseEstateContractEntity
    {
        public EstateContractGuarantorEntity(
            string contractName, 
            PriceValueObject rentPrice, 
            int renterId, 
            int tenantId, 
            int guarantorId) : base(contractName, rentPrice)
        {
            RenterId = renterId;
            TenantId = tenantId;
            GuarantorId = guarantorId;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsTrue(RenterTenantIdsValidation(), "RenterId/TenantId", "RenterId and TenantId cannot be equal")
            );
        }

        public int RenterId { get; private set; }
        public int TenantId { get; private set; }
        public int GuarantorId { get; private set; }

        private bool RenterTenantIdsValidation()
        {
            if (RenterId == TenantId)
                return false;

            return true;
        }
    }
}
