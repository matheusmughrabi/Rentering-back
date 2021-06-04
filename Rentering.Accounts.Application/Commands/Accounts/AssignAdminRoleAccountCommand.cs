using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Commands.Accounts
{
    public class AssignAdminRoleAccountCommand : ICommand
    {
        public AssignAdminRoleAccountCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
