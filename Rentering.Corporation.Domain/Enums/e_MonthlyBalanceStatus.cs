using System.ComponentModel;

namespace Rentering.Corporation.Domain.Enums
{
    public enum e_MonthlyBalanceStatus
    {
        [Description("Aguardando todos os participantes.")]
        Pending = 1,
        [Description("Finalizado")]
        Finished = 2
    }
}
