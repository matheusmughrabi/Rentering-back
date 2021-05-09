using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class RenterHandlers : Notifiable,
        ICommandHandler<CreateRenterCommand>,
        ICommandHandler<UpdateRenterCommand>,
        ICommandHandler<DeleteRenterCommand>

    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public RenterHandlers(IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        public ICommandResult Handle(CreateRenterCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var renterEntity = new RenterEntity(command.AccountId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG,
                cpf, address, spouseName, command.SpouseNationality, spouseIdentityRG, spouseCPF);

            if (_contractUnitOfWork.RenterQuery.CheckIfAccountExists(command.AccountId) == false)
                AddNotification("AccountId", "This Account does not exist");

            AddNotifications(name.Notifications);
            AddNotifications(identityRG.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(spouseName.Notifications);
            AddNotifications(spouseIdentityRG.Notifications);
            AddNotifications(spouseCPF.Notifications);
            AddNotifications(renterEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.RenterCUD.Create(renterEntity);

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
                command.Neighborhood,
                command.City,
                command.CEP,
                command.State,
                command.SpouseFirstName,
                command.SpouseLastName,
                command.SpouseNationality,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return createdRenter;
        }

        public ICommandResult Handle(UpdateRenterCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State); 
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var renterEntity = new RenterEntity(command.AccountId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG, cpf, address, spouseName, command.SpouseNationality, spouseIdentityRG, spouseCPF);

            if (_contractUnitOfWork.RenterQuery.CheckIfAccountExists(command.AccountId) == false)
                AddNotification("AccountId", "This Account does not exist");

            AddNotifications(name.Notifications);
            AddNotifications(identityRG.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(spouseName.Notifications);
            AddNotifications(spouseIdentityRG.Notifications);
            AddNotifications(spouseCPF.Notifications);
            AddNotifications(renterEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.RenterCUD.Update(command.Id, renterEntity);

            var updatedRenter = new CommandResult(true, "Renter updated successfuly", new
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
                command.Neighborhood,
                command.City,
                command.CEP,
                command.State,
                command.SpouseFirstName,
                command.SpouseLastName,
                command.SpouseNationality,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return updatedRenter;
        }

        public ICommandResult Handle(DeleteRenterCommand command)
        {
            _contractUnitOfWork.RenterCUD.Delete(command.Id);

            var deletedRenter = new CommandResult(true, "Renter deleted successfuly", new
            {
                RenterId = command.Id
            });

            return deletedRenter;
        }
    }
}
