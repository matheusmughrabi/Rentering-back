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
            EReceiverPaymentStatus? receiverPaymentStatus = null, 
            EPayerPaymentStatus? payerPaymentStatus = null) : base(id)
        {
            ContractId = contractId;
            Month = month;
            RentPrice = rentPrice;

            PayerPaymentStatus = EPayerPaymentStatus.None;

            if (receiverPaymentStatus != null)
                ReceiverPaymentStatus = (EReceiverPaymentStatus)receiverPaymentStatus;
            else
                ReceiverPaymentStatus = EReceiverPaymentStatus.None;

            if (payerPaymentStatus != null)
                PayerPaymentStatus = (EPayerPaymentStatus)payerPaymentStatus;
            else
                PayerPaymentStatus = EPayerPaymentStatus.None;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; private set; }
        [Required]
        public PriceValueObject RentPrice { get; private set; }
        public EReceiverPaymentStatus ReceiverPaymentStatus { get; private set; }
        public EPayerPaymentStatus PayerPaymentStatus { get; private set; }

        public void ExecutePayment()
        {
            if (PayerPaymentStatus == EPayerPaymentStatus.Executed)
            {
                AddNotification("PayerPaymentStatus", "Este pagamento já foi executado.");
                return;
            }

            PayerPaymentStatus = EPayerPaymentStatus.Executed;
        }

        public void AcceptPayment()
        {
            if (ReceiverPaymentStatus == EReceiverPaymentStatus.Accepted)
            {
                AddNotification("ReceiverPaymentStatus", "Este pagamento já foi aceito.");
                return;
            }

            ReceiverPaymentStatus = EReceiverPaymentStatus.Accepted;
        }

        public void RejectPayment()
        {
            if (ReceiverPaymentStatus == EReceiverPaymentStatus.Rejected)
            {
                AddNotification("ReceiverPaymentStatus", "Este pagamento já foi recusado.");
                return;
            }

            ReceiverPaymentStatus = EReceiverPaymentStatus.Rejected;
        }

        public decimal CalculateOwedAmount(DateTime dueDate)
        {
            if (PayerPaymentStatus == EPayerPaymentStatus.Executed)
                return 0M;

            var daysLate = (DateTime.Now - dueDate).Days;

            var factor = (decimal) 0.1 * daysLate;

            var owedAmount = RentPrice.Price * (1 + factor);
            return owedAmount;
        }
    }
}
