using System;

namespace Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults
{
    public class GetContractsOfCurrentUserQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}
