using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories.ObjectsFromDb
{
    public class ContractPaymentFromDb
    {
        public ContractPaymentFromDb(int contractId, DateTime month, RenterPaymentStatus renterPaymentStatus, TentantPaymentStatus tenantPaymentStatus)
        {
            ContractId = contractId;
            Month = month;
            RenterPaymentStatus = renterPaymentStatus;
            TenantPaymentStatus = tenantPaymentStatus;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
        public RenterPaymentStatus RenterPaymentStatus { get; set; }
        public TentantPaymentStatus TenantPaymentStatus { get; set; }
    }
}
