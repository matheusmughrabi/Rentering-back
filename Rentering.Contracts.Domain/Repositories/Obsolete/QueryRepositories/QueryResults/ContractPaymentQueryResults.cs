using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Application.QueryResults
{
    public class GetContractPaymentsQueryResult
    {
        public int ContractId { get; private set; }
        public DateTime Month { get; private set; }
        public e_RenterPaymentStatus RenterPaymentStatus { get; private set; }
        public e_TenantPaymentStatus TenantPaymentStatus { get; private set; }
    }
}
