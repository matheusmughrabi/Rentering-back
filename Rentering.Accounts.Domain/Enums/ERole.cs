using System.ComponentModel;

namespace Rentering.Accounts.Domain.Enums
{
    public enum ERole
    {
        [Description("Usuário comum")]
        RegularUser = 1,
        [Description("Administrador")]
        Admin = 2
    }
}
