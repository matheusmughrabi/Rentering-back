using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class RejectToParticipateCommand : ICommand
    {
        public RejectToParticipateCommand(int contractId)
        {
            ContractId = contractId;
        }

        [JsonIgnore]
        public int AccountId { get; set; }
        public int ContractId { get; set; }
    }
}
