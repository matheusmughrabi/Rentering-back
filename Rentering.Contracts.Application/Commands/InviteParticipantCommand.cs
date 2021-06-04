using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class InviteParticipantCommand : ICommand
    {
        public InviteParticipantCommand(int contractId, int participantAccountId, e_ParticipantRole participantRole)
        {
            ContractId = contractId;
            ParticipantAccountId = participantAccountId;
            ParticipantRole = participantRole;
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int ContractId { get; set; }
        public int ParticipantAccountId { get; set; }
        public e_ParticipantRole ParticipantRole { get; set; }
    }
}
