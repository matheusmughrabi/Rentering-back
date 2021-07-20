using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_ContractParticipantStatus
    {
        [Description("Sem status")]
        None = 1,

        [Description("Pendente")]
        Pending = 2,

        [Description("Aceito")]
        Accepted = 3,

        [Description("Recusado")]
        Rejected = 4
    }
}
