using System.ComponentModel;

namespace Rentering.Corporation.Domain.Enums
{
    public enum e_InvitationStatus
    {
        [Description("Convidado")]
        Invited = 1,
        [Description("Aceito")]
        Accepted = 2,
        [Description("Recusado")]
        Rejected = 3
    }
}
