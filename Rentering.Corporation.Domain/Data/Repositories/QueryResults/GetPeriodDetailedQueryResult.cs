using Rentering.Common.Shared.QueryResults;
using System;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Data.Repositories.QueryResults
{
    public class GetPeriodDetailedQueryResult : IDataResult
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalProfit { get; set; }
        public List<GetPeriodParticipantBalance> ParticipantBalances { get; set; }
        public List<GetPeriodIncome> Incomes { get; set; }
    }

    public class GetPeriodParticipantBalance
    {
        public string FullName { get; set; }
        public decimal Balance { get; set; }
    }

    public class GetPeriodIncome
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
