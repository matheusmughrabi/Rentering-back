using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.Entities
{
    public class AccountContractsEntity : Entity
    {
        protected AccountContractsEntity()
        {
        }

        public AccountContractsEntity(
            int accountId,
            int contractId,
            EParticipantRole participantRole,
            EParticipantStatus? status = null,
            int? id = null) : base(id)
        {
            AccountId = accountId;
            ContractId = contractId;
            ParticipantRole = participantRole;

            if (status != null)
                Status = (EParticipantStatus)status;
            else
                Status = EParticipantStatus.Pending;
        }

        public int AccountId { get; private set; }
        public int ContractId { get; private set; }
        public virtual ContractEntity Contract { get; private set; }
        public EParticipantRole ParticipantRole { get; private set; }
        public EParticipantStatus Status { get; private set; }

        public void AcceptToParticipate()
        {
            if (Status == EParticipantStatus.Rejected)
            {
                AddNotification("Status", "Não é possível aceitar a participação no contrato, pois você já recusou participar.");
                return;
            }

            if (Status == EParticipantStatus.Accepted)
            {
                AddNotification("Status", "Não é possível aceitar novamente a participação no contrato.");
                return;
            }

            Status = EParticipantStatus.Accepted;
        }

        public void RejectToParticipate()
        {
            if (Status == EParticipantStatus.Rejected)
            {
                AddNotification("Status", "Não é possível recusar novamente a participação no contrato.");
                return;
            }

            Status = EParticipantStatus.Rejected;
        }
    }
}
