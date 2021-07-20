using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_ReceiverPaymentStatus
    {
        [Description("Sem status")]
        None = 0,

        [Description("Recusado")]
        Rejected = 1,

        [Description("Aceito")]
        Accepted = 2
    }

    public enum e_PayerPaymentStatus
    {
        [Description("Sem status")]
        None = 0,

        [Description("Pagamento realizado")]
        Executed = 1
    }
}
