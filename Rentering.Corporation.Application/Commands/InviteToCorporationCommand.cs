using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class InviteToCorporationCommand : Command
    {
        public InviteToCorporationCommand(int corporationId, string email, decimal sharedPercentage)
        {
            CorporationId = corporationId;
            Email = email;
            SharedPercentage = sharedPercentage;

            FailFastValidations();
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int CorporationId { get; set; }
        public string Email { get; set; }
        public decimal SharedPercentage { get; set; }

        public override void FailFastValidations()
        {
            AddNotifications(new ValidationContract()
                 .Requires()
                 .IsEmail(Email, "Email", "Email inválido")
                 .IsGreaterThan(SharedPercentage, 0M, "Porcentagem do total", "A porcentagem do total precisa ser maior do que zero.")
             );
        }
    }
}
