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
            int? id = null)
        {
            AccountId = accountId;
            ContractId = contractId;
            ParticipantRole = participantRole;

            if (id != null)
                AssignId((int)id);
        }

        public int AccountId { get; private set; }
        public int ContractId { get; private set; }
        public e_ParticipantRole ParticipantRole { get; private set; }
    }
}
