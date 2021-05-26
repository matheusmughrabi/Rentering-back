using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using Rentering.Common.Shared.Extensions;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        protected EstateContractEntity()
        {
        }

        public EstateContractEntity(
            string contractName,
            AddressValueObject address,
            PropertyRegistrationNumberValueObject propertyRegistrationNumber,
            PriceValueObject rentPrice,
            DateTime rentDueDate,
            DateTime contractStartDate,
            DateTime contractEndDate,
            int? id = null) : base(id)
        {
            ContractName = contractName;
            Address = address;
            PropertyRegistrationNumber = propertyRegistrationNumber;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;

            _participants = new List<AccountContractsEntity>();
            _renters = new List<RenterEntity>();
            _tenants = new List<TenantEntity>();
            _guarantors = new List<GuarantorEntity>();
            _payments = new List<ContractPaymentEntity>();

            ApplyValidations();
        }

        public string ContractName { get; private set; }
        [Required]
        public AddressValueObject Address { get; private set; }
        [Required]
        public PropertyRegistrationNumberValueObject PropertyRegistrationNumber { get; private set; }
        [Required]
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
            //if (Id == 0)
            //{
            //    AddNotification("Id", "ContractId cannot be zero");
            //    return;
            //}

            var isParticipantAlreadyInThisRole = Participants.Any(c => c.AccountId == accountId && c.ParticipantRole == participantRole);

            if (isParticipantAlreadyInThisRole)
            {
                AddNotification("AccountId", $"This account is already { participantRole } in this contract");
                return;
            }

            var contractId = Id;

            if (_participants.Count() == 0)
            {
                var accountContractsEntity = new AccountContractsEntity(accountId, contractId, participantRole, e_ParticipantStatus.Accepted);
                _participants.Add(accountContractsEntity);
            }
            else
            {
                var accountContractsEntity = new AccountContractsEntity(accountId, contractId, participantRole);
                _participants.Add(accountContractsEntity);
            }
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

        public ContractPaymentEntity ExecutePayment(DateTime month)
        {
            var payment = Payments.Where(p => p.Month.Year == month.Year && p.Month.Month == month.Month).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("monthSpan", $"{month} is not registered in payment cycle of this contract");
                return null;
            }

            payment.ExecutePayment();
            return payment;
        }

        public ContractPaymentEntity AcceptPayment(DateTime month)
        {
            var payment = Payments.Where(p => p.Month.Year == month.Year && p.Month.Month == month.Month).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("monthSpan", $"{month} is not registered in payment cycle of this contract");
                return null;
            }

            payment.AcceptPayment();
            return payment;
        }

        public ContractPaymentEntity RejectPayment(DateTime month)
        {
            var payment = Payments.Where(p => p.Month.Year == month.Year && p.Month.Month == month.Month).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("monthSpan", $"{month} is not registered in payment cycle of this contract");
                return null;
            }

            payment.RejectPayment();
            return payment;
        }

        public decimal CurrentOwedAmount()
        {
            var currentPayment = Payments.OrderBy(c => c.Month)
                .Where(c => c.TenantPaymentStatus == e_TenantPaymentStatus.NONE)
                .FirstOrDefault();

            if (currentPayment == null)
            {
                AddNotification("CurrentPayment", $"There are no open rents anymore");
                return 0M;
            }

            var currentOwedAmount = currentPayment.CalculateOwedAmount(RentDueDate);
            return currentOwedAmount;
        }

        public void IncludeParticipants(List<AccountContractsEntity> participants)
        {
            if (participants == null)
                return;

            var contractId = Id;
            bool isThereBadInput = participants.Any(c => c.ContractId != contractId);

            if (isThereBadInput)
            {
                AddNotification("Participants", $"You have provided a participant that does not belong to this contract");
                return;
            }

            _participants = participants;
        }

        public void IncludeRenters(List<RenterEntity> renters)
        {
            if (renters == null)
                return;

            var contractId = Id;
            bool isThereBadInput = renters.Any(c => c.ContractId != contractId);

            if (isThereBadInput)
            {
                AddNotification("Renters", $"You have provided a renter that does not belong to this contract");
                return;
            }

            _renters = renters;
        }

        public void IncludeTenants(List<TenantEntity> tenants)
        {
            if (tenants == null)
                return;

            var contractId = Id;
            bool isThereBadInput = tenants.Any(c => c.ContractId != contractId);

            if (isThereBadInput)
            {
                AddNotification("Tenants", $"You have provided a tenant that does not belong to this contract");
                return;
            }

            _tenants = tenants;
        }

        public void IncludeGuarantors(List<GuarantorEntity> guarantors)
        {
            if (guarantors == null)
                return;

            var contractId = Id;
            bool isThereBadInput = guarantors.Any(c => c.ContractId != contractId);

            if (isThereBadInput)
            {
                AddNotification("Guarantors", $"You have provided a guarantor that does not belong to this contract");
                return;
            }

            _guarantors = guarantors;
        }

        public void IncludeContractPayments(List<ContractPaymentEntity> payments)
        {
            if (payments == null)
                return;

            var contractId = Id;
            bool isThereBadInput = payments.Any(c => c.ContractId != contractId);

            if (isThereBadInput)
            {
                AddNotification("Payments", $"You have provided a payment that does not belong to this contract");
                return;
            }

            _payments = payments;
        }

        private void ApplyValidations()
        {
            var monthSpan = (ContractEndDate - ContractStartDate).GetMonths();

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(ContractName, 3, "ContractName", "Contract name must have at least 3 letters")
                .HasMaxLen(ContractName, 40, "ContractName", "Contract name must have less than 40 letters")
                .IsGreaterOrEqualsThan(monthSpan, 1, "MonthSpan", "Contract month span must be at least 1 month")
            );

            AddNotifications(Address.Notifications);
            AddNotifications(PropertyRegistrationNumber.Notifications);
            AddNotifications(RentPrice.Notifications);
        }
    }
}
