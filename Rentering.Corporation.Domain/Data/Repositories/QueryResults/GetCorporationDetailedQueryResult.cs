using Rentering.Corporation.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Data.Repositories.QueryResults
{
    public class GetCorporationDetailedQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Admin { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public List<Participant> Participants { get; set; }
        public List<MonthlyBalance> MonthlyBalances { get; set; }
    }

    public class Participant
    {
        public string FullName { get; set; }
        public string InvitationStatus { get; set; }
        public decimal SharedPercentage { get; set; }
    }

    public class MonthlyBalance
    {
        public DateTime Month { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
