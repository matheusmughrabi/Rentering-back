using Rentering.Common.Shared.Commands;
using System;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class AddMonthCommand : Command
    {
        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
