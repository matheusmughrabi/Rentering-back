using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;
using System;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class AddMonthCommand : Command
    {
        public AddMonthCommand(int corporationId, DateTime startDate, DateTime endDate, decimal totalProfit)
        {
            CorporationId = corporationId;
            StartDate = startDate;
            EndDate = endDate;
            //TotalProfit = totalProfit;

            //FailFastValidations();
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public override void FailFastValidations()
        //{
        //    AddNotifications(new ValidationContract()
        //         .Requires()
        //         .IsGreaterThan(TotalProfit, 0M, "Preço", "O preço precisar ser maior do que zero")
        //     );
        //}
    }
}
