using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum EReceiverPaymentStatus
    {
        [Description("Sem status")]
        None = 0,

        [Description("Recusado")]
        Rejected = 1,

        [Description("Aceito")]
        Accepted = 2
    }

    public enum EPayerPaymentStatus
    {
        [Description("Sem status")]
        None = 0,

        [Description("Pagamento realizado")]
        Executed = 1
    }
}
