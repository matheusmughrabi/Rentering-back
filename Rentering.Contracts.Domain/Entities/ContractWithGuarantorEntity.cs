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
        private List<ContractPayment> _payments;

        public ContractWithGuarantorEntity(
            string contractName,
            AddressValueObject address,
            PropertyRegistrationNumberValueObject propertyRegistrationNumber,
            PriceValueObject rentPrice,
            DateTime rentDueDate,
            DateTime contractStartDate,
            DateTime contractEndDate,
            int? id = null)
        {
            ContractName = contractName;
            Address = address;
            PropertyRegistrationNumber = propertyRegistrationNumber;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;

            _payments = new List<ContractPayment>();

            var monthSpan = (ContractEndDate - ContractStartDate).GetMonths();
            if (Payments.Count == 0)
                CreatePaymentCycle(monthSpan);

            if (id != null)
                AssignId((int)id);

            ApplyValidations();
        }

        public string ContractName { get; private set; }
        public RenterEntity Renter { get; private set; }
        public TenantEntity Tenant { get; private set; }
        public GuarantorEntity Guarantor { get; private set; }
        public AddressValueObject Address { get; private set; }
        public PropertyRegistrationNumberValueObject PropertyRegistrationNumber { get; private set; }
        public PriceValueObject RentPrice { get; private set; }
        public DateTime RentDueDate { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime ContractEndDate { get; private set; }
        public IReadOnlyCollection<ContractPayment> Payments => _payments.ToArray();

        public void InviteRenter(RenterEntity renter)
        {
            if (renter.RenterStatus != e_ContractParticipantStatus.None)
            {
                AddNotification("Renter", "This renter is already associated to another contract");
                return;
            }

            Renter = renter;
            Renter.UpdateRenterStatusToAwaiting();
        }

        public void InviteTenant(TenantEntity tenant)
        {
            if (tenant.TenantStatus != e_ContractParticipantStatus.None)
            {
                AddNotification("Tenant", "This tenant is already associated to another contract");
                return;
            }

            Tenant = tenant;
            Tenant.UpdateTenantStatusToAwaiting();
        }

        public void InviteGuarantor(GuarantorEntity guarantor)
        {
            if (guarantor.GuarantorStatus != e_ContractParticipantStatus.None)
            {
                AddNotification("Guarantor", "This guarantor is already associated to another contract");
                return;
            }

            Guarantor = guarantor;
            Guarantor.UpdateGuarantorStatusToAwaiting();
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

        public void CreatePaymentCycle(int monthSpan)
        {
            if (monthSpan < 0)
            {
                AddNotification("monthSpan", "Month span must an integer greater than zero");
                return;
            }

            for (int i = 0; i < monthSpan; i++)
                _payments.Add(new ContractPayment(DateTime.Now.AddMonths(i), RentPrice));
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
