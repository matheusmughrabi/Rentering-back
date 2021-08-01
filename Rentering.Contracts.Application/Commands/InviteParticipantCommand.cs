using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class InviteParticipantCommand : Command
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int ContractId { get; set; }
        public string Email { get; set; }
        public int ParticipantRole { get; set; }
    }
}
