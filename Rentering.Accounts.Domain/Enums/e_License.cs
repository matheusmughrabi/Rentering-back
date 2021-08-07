using System.ComponentModel;

namespace Rentering.Accounts.Domain.Enums
{
    public enum e_License
    {
        [Description("Plano gratuito")]
        Free = 1,
        [Description("Plano padrão")]
        Standard = 2,
        [Description("Plano pro")]
        Pro = 3
    }
}
