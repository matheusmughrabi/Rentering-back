using Rentering.Common.Shared.Enums;
using Rentering.Common.Shared.QueryResults;
using Rentering.Corporation.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Data.Repositories.QueryResults
{
    public class GetCorporationDetailedQueryResult : IDataResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Admin { get; set; }
        public bool IsCurrentUserAdmin { get; set; }
        public DateTime CreateDate { get; set; }
        public EnumResult<e_CorporationStatus> Status { get; set; }
        public List<Participant> Participants { get; set; }
        public List<MonthlyBalance> MonthlyBalances { get; set; }
    }

    public class Participant
    {
        public string FullName { get; set; }
        public EnumResult<e_InvitationStatus> InvitationStatus { get; set; }
        public decimal SharedPercentage { get; set; }
    }

    public class MonthlyBalance
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalProfit { get; set; }
        public EnumResult<e_MonthlyBalanceStatus> Status { get; set; }
        public EnumResult<e_ParticipantBalanceStatus> CurrentUserBalanceStatus { get; set; }
        public List<ParticipantBalance> ParticipantBalances { get; set; }
    }

    public class ParticipantBalance
    {
        public string ParticipantName { get; set; }
        public decimal Balance { get; set; }
        public EnumResult<e_ParticipantBalanceStatus> Status { get; set; }
        public string Description { get; set; }
    }
}
