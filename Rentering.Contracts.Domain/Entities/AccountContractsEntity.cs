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
            e_ParticipantRole participantRole,
            e_ParticipantStatus? status = null,
            int? id = null) : base(id)
        {
            AccountId = accountId;
            ContractId = contractId;
            ParticipantRole = participantRole;

            if (status != null)
                Status = (e_ParticipantStatus)status;
            else
                Status = e_ParticipantStatus.Pending;
        }

        public int AccountId { get; private set; }
        public int ContractId { get; private set; }
        public virtual ContractEntity EstateContract { get; private set; }
        public e_ParticipantRole ParticipantRole { get; private set; }
        public e_ParticipantStatus Status { get; private set; }

        public void AcceptToParticipate()
        {
            if (Status == e_ParticipantStatus.Rejected)
            {
                AddNotification("Status", "Não é possível aceitar a participação no contrato, pois você já recusou participar.");
                return;
            }

            if (Status == e_ParticipantStatus.Accepted)
            {
                AddNotification("Status", "Não é possível aceitar novamente a participação no contrato.");
                return;
            }

            Status = e_ParticipantStatus.Accepted;
        }

        public void RejectToParticipate()
        {
            if (Status == e_ParticipantStatus.Rejected)
            {
                AddNotification("Status", "Não é possível recusar novamente a participação no contrato.");
                return;
            }

            Status = e_ParticipantStatus.Rejected;
        }
    }
}
