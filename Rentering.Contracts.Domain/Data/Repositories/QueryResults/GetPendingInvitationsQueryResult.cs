using Rentering.Common.Shared.Enums;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryResults
{
    public class GetPendingInvitationsQueryResult
    {
        public int AccountContractId { get; set; }
        public int ContractId { get; set; }
        public string ContractName { get; set; }
        public string ContractOwner { get; set; }
        public EnumResult<e_ContractState> ContractState { get; set; }
        public string ParticipantRole { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}
