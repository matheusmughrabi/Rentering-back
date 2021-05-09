using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class GuarantorHandlers : Notifiable,
        ICommandHandler<CreateGuarantorCommand>,
        ICommandHandler<UpdateGuarantorCommand>,
        ICommandHandler<DeleteGuarantorCommand>
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public GuarantorHandlers(IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        public ICommandResult Handle(CreateGuarantorCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(command.SpouseCPF);

            var guarantorEntity = new GuarantorEntity(command.AccountId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG,
                cpf, address, spouseName, command.SpouseNationality, command.SpouseOcupation, spouseIdentityRG, spouseCPF);

            if (_contractUnitOfWork.GuarantorQuery.CheckIfAccountExists(command.AccountId) == false)
                AddNotification("AccountId", "This Account does not exist");

            AddNotifications(guarantorEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.GuarantorCUD.Create(guarantorEntity);

            var createdGuarantor = new CommandResult(true, "Guarantor created successfuly", new
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
                command.SpouseOcupation,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return createdGuarantor;
        }

        public ICommandResult Handle(UpdateGuarantorCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var guarantorEntity = new GuarantorEntity(command.AccountId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG, cpf, address, spouseName, command.SpouseNationality, command.SpouseOcupation, spouseIdentityRG, spouseCPF);

            if (_contractUnitOfWork.GuarantorQuery.CheckIfAccountExists(command.AccountId) == false)
                AddNotification("AccountId", "This Account does not exist");

            AddNotifications(name.Notifications);
            AddNotifications(identityRG.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(spouseName.Notifications);
            AddNotifications(spouseIdentityRG.Notifications);
            AddNotifications(spouseCPF.Notifications);
            AddNotifications(guarantorEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.GuarantorCUD.Update(command.Id, guarantorEntity);

            var updatedGuarantor = new CommandResult(true, "Guarantor updated successfuly", new
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
                command.SpouseOcupation,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return updatedGuarantor;
        }

        public ICommandResult Handle(DeleteGuarantorCommand command)
        {
            _contractUnitOfWork.GuarantorCUD.Delete(command.Id);

            var deletedGuarantor = new CommandResult(true, "Guarantor deleted successfuly", new
            {
                GuarantorId = command.Id
            });

            return deletedGuarantor;
        }
    }
}
