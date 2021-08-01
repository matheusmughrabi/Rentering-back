using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class FinishCreationCommand : Command
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
    }
}
