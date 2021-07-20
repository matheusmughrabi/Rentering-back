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
            e_RenterPaymentStatus? renterPaymentStatus = null, 
            e_TenantPaymentStatus? tenantPaymentStatus = null) : base(id)
        {
            ContractId = contractId;
            Month = month;
            RentPrice = rentPrice;

            TenantPaymentStatus = e_TenantPaymentStatus.None;

            if (renterPaymentStatus != null)
                RenterPaymentStatus = (e_RenterPaymentStatus)renterPaymentStatus;
            else
                RenterPaymentStatus = e_RenterPaymentStatus.None;

            if (tenantPaymentStatus != null)
                TenantPaymentStatus = (e_TenantPaymentStatus)tenantPaymentStatus;
            else
                TenantPaymentStatus = e_TenantPaymentStatus.None;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; private set; }
        [Required]
        public PriceValueObject RentPrice { get; private set; }
        public e_RenterPaymentStatus RenterPaymentStatus { get; private set; }
        public e_TenantPaymentStatus TenantPaymentStatus { get; private set; }

        public void ExecutePayment()
        {
            if (TenantPaymentStatus == e_TenantPaymentStatus.Executed)
            {
                AddNotification("TenantPaymentStatus", "Payment has been executed already");
                return;
            }

            TenantPaymentStatus = e_TenantPaymentStatus.Executed;
        }

        public void AcceptPayment()
        {
            if (RenterPaymentStatus == e_RenterPaymentStatus.Accepted)
            {
                AddNotification("RenterPaymentStatus", "Payment is accepted already");
                return;
            }

            RenterPaymentStatus = e_RenterPaymentStatus.Accepted;
        }

        public void RejectPayment()
        {
            if (RenterPaymentStatus == e_RenterPaymentStatus.Rejected)
            {
                AddNotification("RenterPaymentStatus", "Payment is rejected already");
                return;
            }

            RenterPaymentStatus = e_RenterPaymentStatus.Rejected;
        }

        public decimal CalculateOwedAmount(DateTime dueDate)
        {
            if (TenantPaymentStatus == e_TenantPaymentStatus.Executed)
                return 0M;

            var daysLate = (DateTime.Now - dueDate).Days;

            var factor = (decimal) 0.1 * daysLate;

            var owedAmount = RentPrice.Price * (1 + factor);
            return owedAmount;
        }
    }
}
