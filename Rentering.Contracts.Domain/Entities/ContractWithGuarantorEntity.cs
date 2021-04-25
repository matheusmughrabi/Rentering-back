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
            PriceValueObject rentPrice, 
            int renterId, 
            int tenantId, 
            int guarantorId)
        {
            RenterId = renterId;
            RenterStatus = e_ContractParticipantStatus.Pendente;
            TenantId = tenantId;
            TenantStatus = e_ContractParticipantStatus.Pendente;
            GuarantorId = guarantorId;
            GuarantorStatus = e_ContractParticipantStatus.Pendente;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(ContractName, 3, "ContractName", "Contract name must have at least 3 letters")
                .HasMaxLen(ContractName, 40, "ContractName", "Contract name must have less than 40 letters")
            );
        }

        public string ContractName { get; private set; }
        public int RenterId { get; private set; }
        public e_ContractParticipantStatus RenterStatus { get; private set; }
        public int TenantId { get; private set; }
        public e_ContractParticipantStatus TenantStatus { get; private set; }
        public int GuarantorId { get; private set; }
        public e_ContractParticipantStatus GuarantorStatus { get; private set; }
        public AddressValueObject Address { get; set; }
        public PropertyRegistrationNumberValueObject PropertyRegistrationNumber { get; set; }
        public PriceValueObject RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }

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
