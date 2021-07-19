using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_ContractParticipantStatus
    {
        [Description("Sem status")]
        None = 1,

        [Description("Pendente")]
        Pendente = 2,

        [Description("Aceito")]
        Aceito = 3,

        [Description("Recusado")]
        Recusado = 4
    }
}
