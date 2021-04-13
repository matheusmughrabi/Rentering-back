using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class RenterCommandHandlers : Notifiable,
        ICommandHandler<CreateRenterCommand>,
        ICommandHandler<DeleteRenterCommand>
    {
        private readonly IRenterCUDRepository _renterCUDRepository;

        public RenterCommandHandlers(IRenterCUDRepository renterCUDRepository)
        {
            _renterCUDRepository = renterCUDRepository;
        }

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

            if (_renterCUDRepository.CheckIfAccountExists(command.AccountId) == false)
                AddNotification("AccountId", "This Account does not exist");

            AddNotifications(renterEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _renterCUDRepository.CreateRenter(renterEntity);

            var createdRenter = new CommandResult(true, "Renter created successfuly", new
            {
                command.AccountId,
                command.FirstName,
                command.LastName,
                command.Nationality,
                command.Ocupation,
                command.MaritalStatus,
                command.IdentityRG,
                command.CPF,
                command.Street,
                command.Bairro,
                command.Cidade,
                command.CEP,
                command.Estado,
                command.SpouseFirstName,
                command.SpouseLastName,
                command.SpouseNationality,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return createdRenter;
        }

        public ICommandResult Handle(DeleteRenterCommand command)
        {
            _renterCUDRepository.DeleteRenter(command.Id);

            var deletedRenter = new CommandResult(true, "Renter deleted successfuly", new
            {
                RenterId = command.Id
            });

            return deletedRenter;
        }
    }
}
