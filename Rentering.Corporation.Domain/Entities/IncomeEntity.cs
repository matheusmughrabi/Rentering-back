using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;

namespace Rentering.Corporation.Domain.Entities
{
    public class IncomeEntity : Entity
    {
        protected IncomeEntity()
        {
        }

        public IncomeEntity(string title, string description, decimal value, int monthlyBalanceId)
        {
            Title = title;
            Description = description;
            Value = value;
            MonthlyBalanceId = monthlyBalanceId;

            ApplyValidations();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public int MonthlyBalanceId { get; private set; }
        public virtual MonthlyBalanceEntity MonthlyBalance { get; private set; }

        private void ApplyValidations()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .IsGreaterThan(Value, 0, "Valor", "O valor da entrada precisa ser maior do que zero.")
            );
        }
    }
}
