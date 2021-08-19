using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum EContractState
    {
        [Description("Número insuficiente de participantes")]
        NotEnoughParticipants = 1,

        [Description("Aguardando todos a aceitação de todos os participantes")]
        WaitingParticipantsAccept = 2,

        [Description("Pronto para ser ativado")]
        ReadyForActivation = 3,

        [Description("Ativo")]
        Active = 4,

        [Description("Expirado")]
        Expired = 5
    }
}
