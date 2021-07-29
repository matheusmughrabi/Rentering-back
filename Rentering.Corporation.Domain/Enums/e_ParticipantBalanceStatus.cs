using System.ComponentModel;

namespace Rentering.Corporation.Domain.Enums
{
    public enum e_ParticipantBalanceStatus
    {
        [Description("Aprovação pendente")]
        Pending = 1,
        [Description("Aceito")]
        Accepted = 2,
        [Description("Recusado")]
        Rejected = 3
    }
}
