using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class RenterCommandHandlers : Notifiable,
        ICommandHandler<CreateRenterCommand>
    {
        public ICommandResult Handle(CreateRenterCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Bairro, command.Cidade, command.CEP, command.Estado);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(command.SpouseCPF);

            var renterEntity = new RenterEntity(command.AccountId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG,
                cpf, address, spouseName, command.SpouseNationality, spouseIdentityRG, spouseCPF);

            //if (_userRepository.CheckIfAccountExists(command.AccountId) == false)
            //    AddNotification("AccountId", "This Account does not exist");

            //AddNotifications(userEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            //_userRepository.CreateContractUserProfile(userEntity);

            var createdContractProfileUser = new CommandResult(true, "Profile created successfuly", new
            {
                command.AccountId
            });

            return createdContractProfileUser;
        }
    }
}
