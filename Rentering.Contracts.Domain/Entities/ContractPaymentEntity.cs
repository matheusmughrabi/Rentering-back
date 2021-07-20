using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractPaymentEntity : Entity
    {
        protected ContractPaymentEntity()
        {
        }

        public ContractPaymentEntity(
            int contractId, 
            DateTime month, 
            PriceValueObject rentPrice, 
            int? id = null, 
            e_ReceiverPaymentStatus? receiverPaymentStatus = null, 
            e_PayerPaymentStatus? payerPaymentStatus = null) : base(id)
        {
            ContractId = contractId;
            Month = month;
            RentPrice = rentPrice;

            PayerPaymentStatus = e_PayerPaymentStatus.None;

            if (receiverPaymentStatus != null)
                ReceiverPaymentStatus = (e_ReceiverPaymentStatus)receiverPaymentStatus;
            else
                ReceiverPaymentStatus = e_ReceiverPaymentStatus.None;

            if (payerPaymentStatus != null)
                PayerPaymentStatus = (e_PayerPaymentStatus)payerPaymentStatus;
            else
                PayerPaymentStatus = e_PayerPaymentStatus.None;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; private set; }
        [Required]
        public PriceValueObject RentPrice { get; private set; }
        public e_ReceiverPaymentStatus ReceiverPaymentStatus { get; private set; }
        public e_PayerPaymentStatus PayerPaymentStatus { get; private set; }

        public void ExecutePayment()
        {
            if (PayerPaymentStatus == e_PayerPaymentStatus.Executed)
            {
                AddNotification("PayerPaymentStatus", "Este pagamento já foi executado.");
                return;
            }

            PayerPaymentStatus = e_PayerPaymentStatus.Executed;
        }

        public void AcceptPayment()
        {
            if (ReceiverPaymentStatus == e_ReceiverPaymentStatus.Accepted)
            {
                AddNotification("ReceiverPaymentStatus", "Este pagamento já foi aceito.");
                return;
            }

            ReceiverPaymentStatus = e_ReceiverPaymentStatus.Accepted;
        }

        public void RejectPayment()
        {
            if (ReceiverPaymentStatus == e_ReceiverPaymentStatus.Rejected)
            {
                AddNotification("ReceiverPaymentStatus", "Este pagamento já foi recusado.");
                return;
            }

            ReceiverPaymentStatus = e_ReceiverPaymentStatus.Rejected;
        }

        public decimal CalculateOwedAmount(DateTime dueDate)
        {
            if (PayerPaymentStatus == e_PayerPaymentStatus.Executed)
                return 0M;

            var daysLate = (DateTime.Now - dueDate).Days;

            var factor = (decimal) 0.1 * daysLate;

            var owedAmount = RentPrice.Price * (1 + factor);
            return owedAmount;
        }
    }
}
