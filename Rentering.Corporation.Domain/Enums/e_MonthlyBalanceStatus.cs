using System.ComponentModel;

namespace Rentering.Corporation.Domain.Enums
{
    public enum e_MonthlyBalanceStatus
    {
        [Description("Em andamento")]
        OnGoing = 1,
        [Description("Aguardando")]
        Pending = 2,
        [Description("Finalizado")]
        Finished = 3
    }
}
