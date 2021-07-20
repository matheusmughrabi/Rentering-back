using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_ParticipantStatus
    {
        [Description("Pendente")]
        Pending = 1,

        [Description("Aceito")]
        Accepted = 2,

        [Description("Recusado")]
        Rejected = 3
    }
}
