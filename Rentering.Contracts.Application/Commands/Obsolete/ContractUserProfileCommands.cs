using FluentValidator;
using Rentering.Common.Shared.Commands;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateContractUserProfileCommand : Notifiable, ICommand
    {
        public CreateContractUserProfileCommand(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; set; }
    }
}
