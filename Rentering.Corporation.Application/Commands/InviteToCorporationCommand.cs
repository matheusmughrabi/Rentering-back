using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class InviteToCorporationCommand : ICommand
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public string Email { get; set; }
        public decimal SharedPercentage { get; set; }
    }
}
