using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_ContractState
    {
        [Description("Número insuficiente de participantes")]
        NotEnoughParticipants = 1,

        [Description("Aguardando todos a aceitação de todos os participantes")]
        WaitingParticipantsAccept = 2,

        [Description("Ativo")]
        Active = 3,

        [Description("Expirado")]
        Expired = 4
    }
}
