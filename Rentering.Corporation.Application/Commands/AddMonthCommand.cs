using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;
using System;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class AddMonthCommand : Command
    {
        public AddMonthCommand(int corporationId, DateTime startDate, DateTime endDate)
        {
            CorporationId = corporationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
