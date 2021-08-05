using Rentering.Common.Shared.Enums;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryResults
{
    public class GetContractsOfCurrentUserQueryResult
    {
        public int Id { get; set; }
        public string ContractName { get; set; }
        public EnumResult<e_ContractState> ContractState { get; set; }
        public EnumResult<e_ParticipantRole> ParticipantRole { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}
