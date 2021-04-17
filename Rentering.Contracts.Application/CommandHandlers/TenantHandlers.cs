﻿using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class TenantHandlers : Notifiable,
        ICommandHandler<CreateTenantCommand>
    {
        private readonly ITenantCUDRepository _tenantCUDRepository;

        public TenantHandlers(ITenantCUDRepository tenantCUDRepository)
        {
            _tenantCUDRepository = tenantCUDRepository;
        }

        public ICommandResult Handle(CreateTenantCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(command.SpouseCPF);

            var tenantEntity = new TenantEntity(command.AccountId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG,
                cpf, address, spouseName, command.SpouseNationality, command.SpouseOcupation, spouseIdentityRG, spouseCPF);

            if (_tenantCUDRepository.CheckIfAccountExists(command.AccountId) == false)
                AddNotification("AccountId", "This Account does not exist");

            AddNotifications(tenantEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _tenantCUDRepository.CreateTenant(tenantEntity);

            var createdContractProfileUser = new CommandResult(true, "Profile created successfuly", new
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

            return createdContractProfileUser;
        }
    }
}
