using Rentering.Common.Shared.Commands;

namespace Rentering.Contracts.Application.Authorization.Commands
{
    public class AuthCurrentUserAndProfileRenterMatchCommand : ICommand
    {
        public AuthCurrentUserAndProfileRenterMatchCommand(int accountId, int renterId)
        {
            AccountId = accountId;
            RenterId = renterId;
        }

        public int AccountId { get; set; }
        public int RenterId { get; set; }
    }
}
