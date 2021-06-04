using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults
{
    public class GetPaymentsOfContractQueryResult
    {
        public int ContractId { get; set; }
        public DateTime Month { get; set; }
        public decimal RentPrice { get; set; }
        public e_RenterPaymentStatus RenterPaymentStatus { get; set; }
        public e_TenantPaymentStatus TenantPaymentStatus { get; set; }
    }
}
