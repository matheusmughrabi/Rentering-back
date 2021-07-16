﻿using FluentValidator.Validation;
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
        public AddressValueObject Address { get; private set; } // Remover
        [Required]
        public PropertyRegistrationNumberValueObject PropertyRegistrationNumber { get; private set; } // Remover
        [Required]
        public PriceValueObject RentPrice { get; private set; }
        public DateTime RentDueDate { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime ContractEndDate { get; private set; }
        public IReadOnlyCollection<AccountContractsEntity> Participants => _participants.ToArray();
        public IReadOnlyCollection<RenterEntity> Renters => _renters.ToArray(); // Remover
        public IReadOnlyCollection<TenantEntity> Tenants => _tenants.ToArray(); // Remover
        public IReadOnlyCollection<GuarantorEntity> Guarantors => _guarantors.ToArray(); // Remover
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

        public void AddRenter(RenterEntity renter)
        {
            if (renter == null)
            {
                AddNotification("renter", "Renter cannot be null");
                return;
            }

            if (_renters.Any(c => c.Name.FirstName == renter.Name.FirstName && c.Name.LastName == renter.Name.LastName))
                AddNotification("FullName", "Renter with this name and last name already exists in this contract");

            if (_renters.Any(c => c.IdentityRG.IdentityRG == renter.IdentityRG.IdentityRG))
                AddNotification("CPF", "Renter with this IdentityRG already exists in this contract");

            if (_renters.Any(c => c.CPF.CPF == renter.CPF.CPF))
                AddNotification("CPF", "Renter with this CPF already exists in this contract");

            if (Invalid)
                return;

            _renters.Add(renter);
        }

        public void AddTenant(TenantEntity tenant)
        {
            if (tenant == null)
            {
                AddNotification("tenant", "Tenant cannot be null");
                return;
            }

            if (_tenants.Any(c => c.Name.FirstName == tenant.Name.FirstName && c.Name.LastName == tenant.Name.LastName))
                AddNotification("FullName", "Tenant with this name and last name already exists in this contract");

            if (_tenants.Any(c => c.IdentityRG.IdentityRG == tenant.IdentityRG.IdentityRG))
                AddNotification("CPF", "Tenant with this IdentityRG already exists in this contract");

            if (_tenants.Any(c => c.CPF.CPF == tenant.CPF.CPF))
                AddNotification("CPF", "Tenant with this CPF already exists in this contract");

            if (Invalid)
                return;

            _tenants.Add(tenant);
        }

        public void AddGuarantor(GuarantorEntity guarantor)
        {
            if (guarantor == null)
            {
                AddNotification("guarantor", "Guarantor cannot be null");
                return;
            }

            if (_guarantors.Any(c => c.Name.FirstName == guarantor.Name.FirstName && c.Name.LastName == guarantor.Name.LastName))
                AddNotification("FullName", "Guarantor with this name and last name already exists in this contract");

            if (_guarantors.Any(c => c.IdentityRG.IdentityRG == guarantor.IdentityRG.IdentityRG))
                AddNotification("CPF", "Guarantor with this IdentityRG already exists in this contract");

            if (_guarantors.Any(c => c.CPF.CPF == guarantor.CPF.CPF))
                AddNotification("CPF", "Guarantor with this CPF already exists in this contract");

            if (Invalid)
                return;

            _guarantors.Add(guarantor);
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

                // TODO - new PriceValueObject(RentPrice.Price)
                _payments.Add(new ContractPaymentEntity(Id, monthToBeAdded, new PriceValueObject(RentPrice.Price)));
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
