using Rentering.Common.Shared.Entities;
using Rentering.Corporation.Domain.Enums;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Entities
{
    public class ParticipantEntity : Entity
    {
        private List<ParticipantBalanceEntity> _participantBalances;

        public ParticipantEntity(int accountId, int corporationId, decimal sharedPercentage)
        {
            AccountId = accountId;
            CorporationId = corporationId;
            SharedPercentage = sharedPercentage;
            InvitationStatus = EInvitationStatus.Invited;
        }

        public int AccountId { get; private set; }
        public int CorporationId { get; private set; }
        public virtual CorporationEntity Corporation { get; private set; }
        public EInvitationStatus InvitationStatus { get; private set; }
        public decimal SharedPercentage { get; private set; }
        public IReadOnlyCollection<ParticipantBalanceEntity> ParticipantBalances => _participantBalances.ToArray();

        public void AcceptToParticipate()
        {
            if (InvitationStatus == EInvitationStatus.Rejected)
            {
                AddNotification("Status", "Você já recusou a participação nesta corporação!");
                return;
            }

            if (InvitationStatus == EInvitationStatus.Accepted)
            {
                AddNotification("Status", "Você já aceitou a participação nesta corporação!");
                return;
            }

            InvitationStatus = EInvitationStatus.Accepted;
        }

        public void RejectToParticipate()
        {
            if (InvitationStatus == EInvitationStatus.Rejected)
            {
                AddNotification("Status", "Você já recusou a participação nesta corporação!");
                return;
            }

            InvitationStatus = EInvitationStatus.Rejected;
        }
    }
}
