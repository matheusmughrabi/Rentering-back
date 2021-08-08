using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Accounts.Application.Commands
{
    public class PayLicenseCommand : Command
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int License { get; set; }
    }
}
