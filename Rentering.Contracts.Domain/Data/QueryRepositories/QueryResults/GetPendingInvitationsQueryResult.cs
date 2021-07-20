using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults
{
    public class GetPendingInvitationsQueryResult
    {
        public int Id { get; set; }
        public string ContractName { get; set; }
        public string ContractOwner { get; set; }
        public string ContractState { get; set; }
        public string ParticipantRole { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}
