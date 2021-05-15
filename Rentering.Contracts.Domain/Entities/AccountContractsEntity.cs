using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.Entities
{
    public class AccountContractsEntity : Entity
    {
        public AccountContractsEntity(
            int accountId,
            int contractId,
             e_ParticipantRole participantRole,
            int? id = null) : base(id)
        {
            AccountId = accountId;
            ContractId = contractId;
            ParticipantRole = participantRole;

            Status = e_ParticipantStatus.Invited;
        }

        public int AccountId { get; private set; }
        public int ContractId { get; private set; }
        public e_ParticipantRole ParticipantRole { get; private set; }
        public e_ParticipantStatus Status { get; private set; }
    }
}
