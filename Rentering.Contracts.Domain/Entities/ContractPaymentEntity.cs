using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractPaymentEntity : Entity
    {
        public ContractPaymentEntity(
            int contractId, 
            DateTime month, 
            PriceValueObject rentPrice, 
            int? id = null, 
            e_RenterPaymentStatus? renterPaymentStatus = null, 
            e_TenantPaymentStatus? tenantPaymentStatus = null)
        {
            ContractId = contractId;
            Month = month;
            RentPrice = rentPrice;

            TenantPaymentStatus = e_TenantPaymentStatus.NONE;

            if (id != null)
                AssignId((int)id);

            if (renterPaymentStatus != null)
                RenterPaymentStatus = (e_RenterPaymentStatus)renterPaymentStatus;
            else
                RenterPaymentStatus = e_RenterPaymentStatus.NONE;

            if (tenantPaymentStatus != null)
                TenantPaymentStatus = (e_TenantPaymentStatus)tenantPaymentStatus;
            else
                TenantPaymentStatus = e_TenantPaymentStatus.NONE;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; private set; }
        public PriceValueObject RentPrice { get; private set; }
        public e_RenterPaymentStatus RenterPaymentStatus { get; private set; }
        public e_TenantPaymentStatus TenantPaymentStatus { get; private set; }

        public void ExecutePayment()
        {
            if (TenantPaymentStatus == e_TenantPaymentStatus.EXECUTED)
            {
                AddNotification("TenantPaymentStatus", "Payment has been executed already");
                return;
            }

            TenantPaymentStatus = e_TenantPaymentStatus.EXECUTED;
        }

        public void AcceptPayment()
        {
            if (RenterPaymentStatus == e_RenterPaymentStatus.ACCEPTED)
            {
                AddNotification("RenterPaymentStatus", "Payment is accepted already");
                return;
            }

            RenterPaymentStatus = e_RenterPaymentStatus.ACCEPTED;
        }

        public void RejectPayment()
        {
            if (RenterPaymentStatus == e_RenterPaymentStatus.REJECTED)
            {
                AddNotification("RenterPaymentStatus", "Payment is rejected already");
                return;
            }

            RenterPaymentStatus = e_RenterPaymentStatus.REJECTED;
        }

        public decimal CalculateOwedAmount(DateTime dueDate)
        {
            if (TenantPaymentStatus == e_TenantPaymentStatus.EXECUTED)
                return 0M;

            var daysLate = (DateTime.Now - dueDate).Days;

            var factor = (decimal) 0.1 * daysLate;

            var owedAmount = RentPrice.Price * (1 + factor);
            return owedAmount;
        }
    }
}
