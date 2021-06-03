using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class RejectToParticipateCommandEF : ICommand
    {
        public RejectToParticipateCommandEF(int contractId)
        {
            ContractId = contractId;
        }

        [JsonIgnore]
        public int AccountId { get; set; }
        public int ContractId { get; set; }
    }
}
