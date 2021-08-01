using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class AddMonthCommand : Command
    {
        public AddMonthCommand(int corporationId, decimal totalProfit)
        {
            CorporationId = corporationId;
            TotalProfit = totalProfit;

            FailFastValidations();
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public decimal TotalProfit { get; set; }

        public override void FailFastValidations()
        {
            AddNotifications(new ValidationContract()
                 .Requires()
                 .IsGreaterThan(TotalProfit, 0M, "Preço", "O preço precisar ser maior do que zero")
             );
        }
    }
}
