using Rentering.Common.Shared.Enums;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryResults
{
    public class GetPaymentsOfContractQueryResult
    {
        public int ContractId { get; set; }
        public DateTime Month { get; set; }
        public decimal RentPrice { get; set; }
        public EnumResult<e_ReceiverPaymentStatus> ReceiverPaymentStatus { get; set; }
        public EnumResult<e_PayerPaymentStatus> PayerPaymentStatus { get; set; }
    }
}
