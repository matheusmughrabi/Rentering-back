using System.ComponentModel;

namespace Rentering.Corporation.Domain.Enums
{
    public enum ECorporationStatus
    {
        [Description("Processo de criação em andamento")]
        InProgress = 1,
        [Description("Aguardo todos os participantes aceitarem")]
        WaitingParticipants = 2,
        [Description("Pronto para ativação")]
        ReadyForActivation = 3,
        [Description("Ativo")]
        Active = 4
    }
}
