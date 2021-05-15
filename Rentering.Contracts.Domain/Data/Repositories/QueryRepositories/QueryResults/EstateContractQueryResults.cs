using Rentering.Common.Shared.Queries;
using Rentering.Contracts.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults
{
    public class GetEstateContractQueryResult : IQueryResult
    {
        // TODO - Corrigir RenterId, TenantId, GuarantorId aqui
        public int Id { get; set; }
        public string ContractName { get; set; }
        public int RenterId { get; set; }
        public int TenantId { get; set; }
        public int GuarantorId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string CEP { get; set; }
        public e_BrazilStates State { get; set; }
        public int PropertyRegistrationNumber { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }

    public class GetCurrentUserContractQueryResult : IQueryResult
    {
        public string ContractName { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string CEP { get; set; }
        public e_BrazilStates State { get; set; }
        public int PropertyRegistrationNumber { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public ICollection<ContractParticipants> Participants { get; set; }
    }

    public class ContractParticipants
    {
        public string Username { get; set; }
        public e_ParticipantRole ParticipantRole { get; set; }
        public e_ContractParticipantStatus Status { get; set; }
    }

    public class GetContractNameQueryResult : IQueryResult
    {
        public string ContractName { get; set; }
    }

    public class GetPendingInvitations : IQueryResult
    {
        public string ContractName { get; set; }
        public e_ParticipantRole ParticipantRole { get; set; }
        public e_ContractParticipantStatus Status { get; set; }
    }
}
