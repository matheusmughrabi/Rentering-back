using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class RegisterIncomeCommand : Command
    {
        public RegisterIncomeCommand(int corporationId, int monthlyBalanceId, string title, string description, decimal value)
        {
            CorporationId = corporationId;
            MonthlyBalanceId = monthlyBalanceId;
            Title = title;
            Description = description;
            Value = value;

            FailFastValidations();
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public int MonthlyBalanceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }

        public override void FailFastValidations()
        {
            AddNotifications(new ValidationContract()
                 .Requires()
                 .IsGreaterThan(Value, 0M, "Preço", "O valor da renda precisar ser maior do que zero")
             );
        }
    }
}
