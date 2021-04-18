using Rentering.Common.Shared.Commands;

namespace Rentering.Contracts.Application.Authorization.Commands
{
    public class AuthCurrentUserAndProfileGuarantorMatchCommand : ICommand
    {
        public AuthCurrentUserAndProfileGuarantorMatchCommand(int accountId, int guarantorId)
        {
            AccountId = accountId;
            GuarantorId = guarantorId;
        }

        public int AccountId { get; set; }
        public int GuarantorId { get; set; }
    }
}
