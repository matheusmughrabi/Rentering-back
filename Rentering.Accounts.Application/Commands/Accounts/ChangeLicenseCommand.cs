using Rentering.Accounts.Domain.Enums;
using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Accounts.Application.Commands.Accounts
{
    public class ChangeLicenseCommand : Command
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int License { get; set; }
    }
}
