using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.Handlers
{
    public class TenantHandlers : Notifiable,
        IHandler<CreateTenantCommand>,
        IHandler<UpdateTenantCommand>,
        IHandler<DeleteTenantCommand>
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public TenantHandlers(IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        public ICommandResult Handle(CreateTenantCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var tenantEntity = new TenantEntity(command.ContractId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG,
                cpf, address, spouseName, command.SpouseNationality, command.SpouseOcupation, spouseIdentityRG, spouseCPF);

            // TODO - Criar CheckIfContractExists
            //if (_contractUnitOfWork.TenantQuery.CheckIfAccountExists(command.AccountId) == false)
            //    AddNotification("AccountId", "This Account does not exist");

            AddNotifications(tenantEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.TenantCUD.Create(tenantEntity);

            var createdContractProfileUser = new CommandResult(true, "Profile created successfuly", new
            {
                command.ContractId,
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

            return createdContractProfileUser;
        }

        public ICommandResult Handle(UpdateTenantCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var tenantEntity = new TenantEntity(command.ContractId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG, cpf, address, spouseName, command.SpouseNationality, command.SpouseOcupation, spouseIdentityRG, spouseCPF);

            // TODO - Criar CheckIfContractExists
            //if (_contractUnitOfWork.TenantQuery.CheckIfAccountExists(command.AccountId) == false)
            //    AddNotification("AccountId", "This Account does not exist");

            AddNotifications(name.Notifications);
            AddNotifications(identityRG.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(spouseName.Notifications);
            AddNotifications(spouseIdentityRG.Notifications);
            AddNotifications(spouseCPF.Notifications);
            AddNotifications(tenantEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.TenantCUD.Update(command.Id, tenantEntity);

            var updatedTenant = new CommandResult(true, "Tenant updated successfuly", new
            {
                command.ContractId,
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

            return updatedTenant;
        }

        public ICommandResult Handle(DeleteTenantCommand command)
        {
            _contractUnitOfWork.TenantCUD.Delete(command.Id);

            var deletedTenant = new CommandResult(true, "Tenant deleted successfuly", new
            {
                GuarantorId = command.Id
            });

            return deletedTenant;
        }
    }
}
