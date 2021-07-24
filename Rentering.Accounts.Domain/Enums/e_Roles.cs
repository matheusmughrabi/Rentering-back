using System.ComponentModel;

namespace Rentering.Accounts.Domain.Enums
{
    public enum e_Roles
    {
        [Description("Usuário comum")]
        RegularUser = 1,
        [Description("Administrador")]
        Admin = 2
    }
}