using Rentering.Common.Shared.Entities;
using Rentering.Corporation.Domain.Enums;

namespace Rentering.Corporation.Domain.Entities
{
    public class ParticipantEntity : Entity
    {
        public ParticipantEntity(int accountId, int corporationId, decimal sharedPercentage)
        {
            AccountId = accountId;
            CorporationId = corporationId;
            SharedPercentage = sharedPercentage;
            InvitationStatus = e_InvitationStatus.Invited;
        }

        public int AccountId { get; private set; }
        public int CorporationId { get; private set; }
        public virtual CorporationEntity Corporation { get; private set; }
        public e_InvitationStatus InvitationStatus { get; private set; }
        public decimal SharedPercentage { get; private set; }

        public void AcceptToParticipate()
        {
            if (InvitationStatus == e_InvitationStatus.Rejected)
            {
                AddNotification("Status", "Você já recusou a participação nesta corporação!");
                return;
            }

            if (InvitationStatus == e_InvitationStatus.Accepted)
            {
                AddNotification("Status", "Você já aceitou a participação nesta corporação!");
                return;
            }

            InvitationStatus = e_InvitationStatus.Accepted;
        }

        public void RejectToParticipate()
        {
            if (InvitationStatus == e_InvitationStatus.Rejected)
            {
                AddNotification("Status", "Você já recusou a participação nesta corporação!");
                return;
            }

            InvitationStatus = e_InvitationStatus.Rejected;
        }
    }
}
