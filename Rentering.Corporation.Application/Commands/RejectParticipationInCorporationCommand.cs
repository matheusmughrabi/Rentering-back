using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class RejectParticipationInCorporationCommand : Command
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public int ParticipantId { get; set; }
    }
}
