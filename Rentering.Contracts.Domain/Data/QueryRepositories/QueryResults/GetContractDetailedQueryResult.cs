using System;

namespace Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults
{
    public class GetContractDetailedQueryResult
    {
        public GetContractDetailedQueryResult()
        {
        }

        public int Id { get; set; }
        public string ContractName { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}
