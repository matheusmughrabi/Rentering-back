using System.ComponentModel;

namespace Rentering.Corporation.Domain.Enums
{
    public enum EInvitationStatus
    {
        [Description("Convidado")]
        Invited = 1,
        [Description("Aceito")]
        Accepted = 2,
        [Description("Recusado")]
        Rejected = 3
    }
}
