using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using Rentering.Common.Shared.Extensions;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractWithGuarantorEntity : Entity
    {
        private List<ContractPaymentEntity> _payments;

        public ContractWithGuarantorEntity(
            string contractName,
            AddressValueObject address,
            PropertyRegistrationNumberValueObject propertyRegistrationNumber,
            PriceValueObject rentPrice,
            DateTime rentDueDate,
            DateTime contractStartDate,
            DateTime contractEndDate,
            int? id = null,
            int? renterId = null,
            int? tenantId = null,
            int? guarantorId = null)
        {
            ContractName = contractName;
            Address = address;
            PropertyRegistrationNumber = propertyRegistrationNumber;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;


            if (id != null)
                AssignId((int)id);

            if (renterId != null)
                RenterId = (int)renterId;

            if (tenantId != null)
                TenantId = (int)tenantId;

            if (guarantorId != null)
                GuarantorId = (int)guarantorId;

            ApplyValidations();
        }

        public string ContractName { get; private set; }
        public int RenterId { get; private set; }
        public int TenantId { get; private set; }
        public int GuarantorId { get; private set; }
        public AddressValueObject Address { get; private set; }
        public PropertyRegistrationNumberValueObject PropertyRegistrationNumber { get; private set; }
        public PriceValueObject RentPrice { get; private set; }
        public DateTime RentDueDate { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime ContractEndDate { get; private set; }
        public IReadOnlyCollection<ContractPaymentEntity> Payments => _payments.ToArray();

        public void InviteRenter(RenterEntity renter)
        {
            if (renter.RenterStatus != e_ContractParticipantStatus.None)
            {
                AddNotification("Renter", "This renter is already associated to another contract");
                return;
            }

            renter.UpdateRenterStatusToAwaiting();

            if (renter.Valid == false)
                return;

            RenterId = renter.Id;
        }

        public void InviteTenant(TenantEntity tenant)
        {
            if (tenant.TenantStatus != e_ContractParticipantStatus.None)
            {
                AddNotification("Tenant", "This tenant is already associated to another contract");
                return;
            }

            tenant.UpdateTenantStatusToAwaiting();

            if (tenant.Valid == false)
                return;

            TenantId = tenant.Id;
        }

        public void InviteGuarantor(GuarantorEntity guarantor)
        {
            if (guarantor.GuarantorStatus != e_ContractParticipantStatus.None)
            {
                AddNotification("Guarantor", "This guarantor is already associated to another contract");
                return;
            }

            guarantor.UpdateGuarantorStatusToAwaiting();

            if (guarantor.Valid == false)
                return;

            GuarantorId = guarantor.Id;
        }

        public void UpdateRentPrice(PriceValueObject rentPrice)
        {
            if (rentPrice.Price < 0)
            {
                AddNotifications(rentPrice.Notifications);
                return;
            }

            RentPrice = rentPrice;
        }

        public void IncludeContractPayments(List<ContractPaymentEntity> payments)
        {
            if (payments != null)
                _payments = payments;
        }

        public void CreatePaymentCycle()
        {
            if (Id == 0)
            {
                AddNotification("Id", "ContractId cannot be null");
                return;
            }

            var monthSpan = (ContractEndDate - ContractStartDate).GetMonths();

            if (monthSpan < 0)
            {
                AddNotification("monthSpan", "Month span must an integer greater than zero");
                return;
            }

            for (int i = 0; i < monthSpan; i++)
            {
                var monthToBeAdded = ContractStartDate.AddMonths(i);

                if (_payments.Any(c => c.Month == monthToBeAdded))
                {
                    AddNotification("monthSpan", $"{monthToBeAdded} is already registered in the payment cycle");
                    continue;
                }

                _payments.Add(new ContractPaymentEntity(Id, monthToBeAdded, RentPrice));
            }
        }

        public void ExecutePayment(DateTime month)
        {
            var payment = Payments.Where(p => p.Month.ToShortDateString() == month.ToShortDateString()).FirstOrDefault();
            payment.ExecutePayment();
        }

        public decimal CurrentOwedAmount()
        {
            var currentPayment = Payments.OrderBy(c => c.Month)
                .Where(c => c.TenantPaymentStatus == e_TenantPaymentStatus.NONE)
                .FirstOrDefault();

            var currentOwedAmount = currentPayment.CalculateOwedAmount(RentDueDate);
            return currentOwedAmount;
        }

        private void ApplyValidations()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(ContractName, 3, "ContractName", "Contract name must have at least 3 letters")
                .HasMaxLen(ContractName, 40, "ContractName", "Contract name must have less than 40 letters")
            );

            AddNotifications(Address.Notifications);
            AddNotifications(PropertyRegistrationNumber.Notifications);
            AddNotifications(RentPrice.Notifications);
        }
    }
}
