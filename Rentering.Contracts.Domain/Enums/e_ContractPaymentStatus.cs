using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_RenterPaymentStatus
    {
        [Description("Sem status")]
        NONE = 0,

        [Description("Recusado")]
        REJECTED = 1,

        [Description("Aceito")]
        ACCEPTED = 2
    }

    public enum e_TenantPaymentStatus
    {
        [Description("Sem status")]
        NONE = 0,

        [Description("Pagamento realizado")]
        EXECUTED = 1
    }
}
