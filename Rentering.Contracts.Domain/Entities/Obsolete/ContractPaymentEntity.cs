using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractPaymentEntity : Entity
    {
        public ContractPaymentEntity(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
            RenterPaymentStatus = e_RenterPaymentStatus.NONE;
            TenantPaymentStatus = e_TentantPaymentStatus.NONE;
        }

        public ContractPaymentEntity(int contractId, DateTime month, e_RenterPaymentStatus renterPaymentStatus, e_TentantPaymentStatus tenantPaymentStatus)
        {
            ContractId = contractId;
            Month = month;
            RenterPaymentStatus = renterPaymentStatus;
            TenantPaymentStatus = tenantPaymentStatus;
        }

        public int ContractId { get; private set; }
        public DateTime Month { get; private set; }
        public e_RenterPaymentStatus RenterPaymentStatus { get; private set; }
        public e_TentantPaymentStatus TenantPaymentStatus { get; private set; }

        public void PayRent()
        {
            if (TenantPaymentStatus == e_TentantPaymentStatus.EXECUTED)
            {
                AddNotification("TenantPaymentStatus", "Payment of this month is already executed");
                return;
            }

            TenantPaymentStatus = e_TentantPaymentStatus.EXECUTED;
        }

        public void AcceptPayment()
        {
            if (TenantPaymentStatus == e_TentantPaymentStatus.NONE)
            {
                AddNotification("TentantPaymentStatus", "You cannot accept this payment because the tenant has not executed it yet");
                return;
            }

            if (RenterPaymentStatus == e_RenterPaymentStatus.ACCEPTED)
            {
                AddNotification("RenterPaymentStatus", "Payment of this month is already accepted");
                return;
            }

            RenterPaymentStatus = e_RenterPaymentStatus.ACCEPTED;
        }

        public void RejectPayment()
        {
            if (TenantPaymentStatus == e_TentantPaymentStatus.NONE)
            {
                AddNotification("TentantPaymentStatus", "You cannot accept this payment because the tenant has not executed it yet");
                return;
            }

            if (RenterPaymentStatus == e_RenterPaymentStatus.REJECTED)
            {
                AddNotification("RenterPaymentStatus", "Payment of this month is already rejected");
                return;
            }

            RenterPaymentStatus = e_RenterPaymentStatus.REJECTED;
        }
    }
}
