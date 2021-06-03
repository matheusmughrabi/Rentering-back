using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Commands.Accounts
{
    public class AssignAdminRoleAccountCommandEF : ICommand
    {
        public AssignAdminRoleAccountCommandEF(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
