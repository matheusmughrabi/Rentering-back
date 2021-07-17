using Rentering.Contracts.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults
{
    public class GetContractDetailedQueryResult
    {
        public int Id { get; set; }
        public string ContractName { get; set; }
        public e_ContractState ContractState { get; set; }
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
        public string Username { get; set; }
        public e_ParticipantRole ParticipantRole { get; set; }
        public e_ParticipantStatus Status { get; set; }
    }

    public class ContractPayment
    {
        public DateTime Month { get; set; }
        public decimal RentPrice { get; set; }
        public e_RenterPaymentStatus RenterPaymentStatus { get; set; }
        public e_TenantPaymentStatus TenantPaymentStatus { get; set; }
    }
}
