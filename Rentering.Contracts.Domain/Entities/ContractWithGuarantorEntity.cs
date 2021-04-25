using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractWithGuarantorEntity : Entity
    {
        public ContractWithGuarantorEntity(
            string contractName,
            int renterId,
            int renterAccountId,
            int tenantId,
            int tenantAccountId,
            int guarantorId,
            int guarantorAccountId, 
            AddressValueObject address, 
            PropertyRegistrationNumberValueObject propertyRegistrationNumber,
            PriceValueObject rentPrice,
            DateTime rentDueDate, 
            DateTime contractStartDate, 
            DateTime contractEndDate)
        {
            ContractName = contractName;
            RenterId = renterId;
            RenterAccountId = renterAccountId;
            RenterStatus = e_ContractParticipantStatus.Pendente;
            TenantId = tenantId;
            TenantAccountId = tenantAccountId;
            TenantStatus = e_ContractParticipantStatus.Pendente;
            GuarantorId = guarantorId;
            GuarantorAccountId = guarantorAccountId;
            GuarantorStatus = e_ContractParticipantStatus.Pendente;
            Address = address;
            PropertyRegistrationNumber = propertyRegistrationNumber;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(ContractName, 3, "ContractName", "Contract name must have at least 3 letters")
                .HasMaxLen(ContractName, 40, "ContractName", "Contract name must have less than 40 letters")
                .IsTrue(RenterTenantIdsValidation(), "RenterId/TentantId", "RenterId and TenantId cannot be equal")
            );

            AddNotifications(Address.Notifications);
            AddNotifications(PropertyRegistrationNumber.Notifications);
            AddNotifications(RentPrice.Notifications);
        }

        public string ContractName { get; private set; }
        public int RenterId { get; private set; }
        public int RenterAccountId { get; private set; }
        public e_ContractParticipantStatus RenterStatus { get; private set; }
        public int TenantId { get; private set; }
        public int TenantAccountId { get; private set; }
        public e_ContractParticipantStatus TenantStatus { get; private set; }
        public int GuarantorId { get; private set; }
        public int GuarantorAccountId { get; private set; }
        public e_ContractParticipantStatus GuarantorStatus { get; private set; }
        public AddressValueObject Address { get; private set; }
        public PropertyRegistrationNumberValueObject PropertyRegistrationNumber { get; private set; }
        public PriceValueObject RentPrice { get; private set; }
        public DateTime RentDueDate { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime ContractEndDate { get; private set; }

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
            if (RenterId == TenantId)
                return false;

            return true;
        }
    }
}
