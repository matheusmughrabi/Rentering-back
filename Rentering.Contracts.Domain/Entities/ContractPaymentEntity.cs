using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractPaymentEntity : BaseEntity
    {
        public ContractPaymentEntity(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
            RenterPaymentStatus = RenterPaymentStatus.NONE;
            TenantPaymentStatus = TentantPaymentStatus.NONE;
        }

        public ContractPaymentEntity(int contractId, DateTime month, RenterPaymentStatus renterPaymentStatus, TentantPaymentStatus tenantPaymentStatus)
        {
            ContractId = contractId;
            Month = month;
            RenterPaymentStatus = renterPaymentStatus;
            TenantPaymentStatus = tenantPaymentStatus;
        }

        public int ContractId { get; private set; }
        public DateTime Month { get; private set; }
        public RenterPaymentStatus RenterPaymentStatus { get; private set; }
        public TentantPaymentStatus TenantPaymentStatus { get; private set; }

        public void PayRent()
        {
            if (TenantPaymentStatus == TentantPaymentStatus.EXECUTED)
            {
                AddNotification("TenantPaymentStatus", "Payment of this month is already executed");
                return;
            }

            TenantPaymentStatus = TentantPaymentStatus.EXECUTED;
        }

        public void AcceptPayment()
        {
            if (TenantPaymentStatus == TentantPaymentStatus.NONE)
            {
                AddNotification("TentantPaymentStatus", "You cannot accept this payment because the tenant has not executed it yet");
                return;
            }

            if (RenterPaymentStatus == RenterPaymentStatus.ACCEPTED)
            {
                AddNotification("RenterPaymentStatus", "Payment of this month is already accepted");
                return;
            }

            RenterPaymentStatus = RenterPaymentStatus.ACCEPTED;
        }

        public void RejectPayment()
        {
            if (TenantPaymentStatus == TentantPaymentStatus.NONE)
            {
                AddNotification("TentantPaymentStatus", "You cannot accept this payment because the tenant has not executed it yet");
                return;
            }

            if (RenterPaymentStatus == RenterPaymentStatus.REJECTED)
            {
                AddNotification("RenterPaymentStatus", "Payment of this month is already rejected");
                return;
            }

            RenterPaymentStatus = RenterPaymentStatus.REJECTED;
        }
    }
}
