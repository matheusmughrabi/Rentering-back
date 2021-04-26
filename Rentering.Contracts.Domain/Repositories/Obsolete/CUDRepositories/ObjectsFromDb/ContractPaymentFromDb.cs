using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories.ObjectsFromDb
{
    public class ContractPaymentFromDb
    {
        public ContractPaymentFromDb(int contractId, DateTime month, e_RenterPaymentStatus renterPaymentStatus, e_TenantPaymentStatus tenantPaymentStatus)
        {
            ContractId = contractId;
            Month = month;
            RenterPaymentStatus = renterPaymentStatus;
            TenantPaymentStatus = tenantPaymentStatus;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
        public e_RenterPaymentStatus RenterPaymentStatus { get; set; }
        public e_TenantPaymentStatus TenantPaymentStatus { get; set; }
    }
}
