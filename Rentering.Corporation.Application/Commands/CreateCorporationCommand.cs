using FluentValidator.Validation;
using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Corporation.Application.Commands
{
    public class CreateCorporationCommand : Command
    {
        public CreateCorporationCommand(string name)
        
        {
            Name = name;
            FailFastValidations();
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public string Name { get; set; }

        public override void FailFastValidations()
        {
            AddNotifications(new ValidationContract()
                 .Requires()
                 .HasMinLen(Name, 3, "Nome da corporação", "O nome da corporação precisa ter entre 3 e 20 letras.")
                 .HasMaxLen(Name, 20, "Nome da corporação", "O nome da corporação precisa ter entre 3 e 20 letras.")
             );
        }
    }
}
