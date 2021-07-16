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
        private List<ContractPaymentEntity> _payments;

        protected EstateContractEntity()
        {
        }

        public EstateContractEntity(
            string contractName,
            PriceValueObject rentPrice,
            DateTime rentDueDate,
            DateTime contractStartDate,
            DateTime contractEndDate,
            int? id = null) : base(id)
        {
            ContractName = contractName;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;

            _participants = new List<AccountContractsEntity>();
            _payments = new List<ContractPaymentEntity>();

            ApplyValidations();
        }

        public string ContractName { get; private set; }
        [Required]
        public PriceValueObject RentPrice { get; private set; }
        public DateTime RentDueDate { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime ContractEndDate { get; private set; }
        public IReadOnlyCollection<AccountContractsEntity> Participants => _participants.ToArray();
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

            AddNotifications(RentPrice.Notifications);
        }
    }
}
