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
        public e_InvitationStatus InvitationStatus { get; private set; }
        public decimal SharedPercentage { get; private set; }
    }
}
