using Rentering.Common.Shared.Enums;
using Rentering.Contracts.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryResults
{
    public class GetContractDetailedQueryResult
    {
        public int Id { get; set; }
        public string ContractName { get; set; }
        public EnumResult<e_ParticipantRole> CurrentUserRole { get; set; }
        public EnumResult<e_ContractState> ContractState { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public List<Participant> Participants { get; set; }
        public List<ContractPayment> ContractPayments { get; set; }
    }

    public class Participant
    {
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public EnumResult<e_ParticipantRole> ParticipantRole { get; set; }
        public EnumResult<e_ParticipantStatus> Status { get; set; }
    }

    public class ContractPayment
    {
        public DateTime Month { get; set; }
        public decimal RentPrice { get; set; }
        public EnumResult<e_ReceiverPaymentStatus> ReceiverPaymentStatus { get; set; }
        public EnumResult<e_PayerPaymentStatus> PayerPaymentStatus { get; set; }
    }
}
