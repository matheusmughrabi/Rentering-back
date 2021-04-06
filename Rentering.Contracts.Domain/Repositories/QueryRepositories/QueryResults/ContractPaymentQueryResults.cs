using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Application.QueryResults
{
    public class GetContractPaymentsQueryResult
    {
        public int ContractId { get; private set; }
        public DateTime Month { get; private set; }
        public RenterPaymentStatus RenterPaymentStatus { get; private set; }
        public TentantPaymentStatus TenantPaymentStatus { get; private set; }
    }
}
