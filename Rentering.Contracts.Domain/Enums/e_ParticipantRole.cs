using System.ComponentModel;

namespace Rentering.Contracts.Domain.Enums
{
    public enum e_ParticipantRole
    {
        [Description("Criador")]
        Owner = 1,

        [Description("Locador")]
        Renter = 2,

        [Description("Locatário")]
        Tenant = 3,

        [Description("Fiador")]
        Guarantor = 4,

        [Description("Convidado")]
        Viewer = 5
    }
}
