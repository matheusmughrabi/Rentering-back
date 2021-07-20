using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_RenterPaymentStatus
    {
        [Description("Sem status")]
        None = 0,

        [Description("Recusado")]
        Rejected = 1,

        [Description("Aceito")]
        Accepted = 2
    }

    public enum e_TenantPaymentStatus
    {
        [Description("Sem status")]
        None = 0,

        [Description("Pagamento realizado")]
        Executed = 1
    }
}
