using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum EParticipantRole
    {
        [Description("Criador")]
        Owner = 1,

        [Description("Recebedor")]
        Receiver = 2,

        [Description("Pagador")]
        Payer = 3,

        [Description("Convidado")]
        Viewer = 5
    }
}
