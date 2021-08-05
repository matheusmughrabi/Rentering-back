using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class GetCurrentOwedAmountCommand : Command
    {
        public GetCurrentOwedAmountCommand(int contractId)
        {
            ContractId = contractId;
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int ContractId { get; set; }
    }
}
