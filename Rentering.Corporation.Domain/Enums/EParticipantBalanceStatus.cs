using System.ComponentModel;

namespace Rentering.Corporation.Domain.Enums
{
    public enum EParticipantBalanceStatus
    {
        [Description("Aprovação pendente")]
        Pending = 1,
        [Description("Aceito")]
        Accepted = 2,
        [Description("Contestado")]
        Rejected = 3
    }
}
