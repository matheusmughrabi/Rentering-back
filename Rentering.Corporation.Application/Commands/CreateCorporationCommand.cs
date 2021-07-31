using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class CreateCorporationCommand : Command
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public string Name { get; set; }
    }
}
