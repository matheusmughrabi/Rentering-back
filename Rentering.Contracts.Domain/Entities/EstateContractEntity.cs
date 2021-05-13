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
    public class EstateContractEntity : Entity
    {
        private List<AccountContractsEntity> _participants;
        private List<RenterEntity> _renters;
        private List<TenantEntity> _tenants;
        private List<GuarantorEntity> _guarantors;
        private List<ContractPaymentEntity> _payments;

        public EstateContractEntity(
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

            if (id != null)
                AssignId((int)id);

            ApplyValidations();
        }

        public string ContractName { get; private set; }
        public AddressValueObject Address { get; private set; }
        public PropertyRegistrationNumberValueObject PropertyRegistrationNumber { get; private set; }
        public PriceValueObject RentPrice { get; private set; }
        public DateTime RentDueDate { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime ContractEndDate { get; private set; }
        public IReadOnlyCollection<AccountContractsEntity> Participants => _participants.ToArray();
        public IReadOnlyCollection<RenterEntity> Renters => _renters.ToArray(); // TODO - Criar IParticipante -> Renter, Tenant, Guarantor
        public IReadOnlyCollection<TenantEntity> Tenants => _tenants.ToArray();
        public IReadOnlyCollection<GuarantorEntity> Guarantors => _guarantors.ToArray();
        public IReadOnlyCollection<ContractPaymentEntity> Payments => _payments.ToArray();

        public void InviteParticipant(int accountId, e_ParticipantRole participantRole)
        {
            var isParticipantAlreadyInThisRole = Participants.Any(c => c.AccountId == accountId && c.ParticipantRole == participantRole);

            if (isParticipantAlreadyInThisRole)
            {
                AddNotification("AccountId", $"This account is already { participantRole } in this contract");
                return;
            }

            var contractId = Id;
            var accountContractsEntity = new AccountContractsEntity(accountId, contractId, participantRole);

            _participants.Add(accountContractsEntity);
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

            if (payment == null)
            {
                AddNotification("monthSpan", $"{month} is not registered in payment cycle of this contract");
                return;
            }
                
            payment.ExecutePayment();
        }

        public void AcceptPayment(DateTime month)
        {
            var payment = Payments.Where(p => p.Month.ToShortDateString() == month.ToShortDateString()).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("monthSpan", $"{month} is not registered in payment cycle of this contract");
                return;
            }

            payment.AcceptPayment();
        }

        public void RejectPayment(DateTime month)
        {
            var payment = Payments.Where(p => p.Month.ToShortDateString() == month.ToShortDateString()).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("monthSpan", $"{month} is not registered in payment cycle of this contract");
                return;
            }

            payment.RejectPayment();
        }

        public decimal CurrentOwedAmount()
        {
            var currentPayment = Payments.OrderBy(c => c.Month)
                .Where(c => c.TenantPaymentStatus == e_TenantPaymentStatus.NONE)
                .FirstOrDefault();

            var currentOwedAmount = currentPayment.CalculateOwedAmount(RentDueDate);
            return currentOwedAmount;
        }

        // TODO - This method should not exist, instead the list of payments should be included in a way similar to EF
        public void IncludeParticipants(List<AccountContractsEntity> participants)
        {
            if (participants != null)
                _participants = participants;
        }

        public void IncludeRenters(List<RenterEntity> renters)
        {
            if (renters != null)
                _renters = renters;
        }

        public void IncludeTenants(List<TenantEntity> tenants)
        {
            if (tenants != null)
                _tenants = tenants;
        }

        public void IncludeGuarantors(List<GuarantorEntity> guarantors)
        {
            if (guarantors != null)
                _guarantors = guarantors;
        }

        public void IncludeContractPayments(List<ContractPaymentEntity> payments)
        {
            if (payments != null)
                _payments = payments;
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
