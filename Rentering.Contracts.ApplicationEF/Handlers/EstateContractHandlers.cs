using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.ApplicationEF.Commands;
using Rentering.Contracts.Domain.DataEF;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Linq;

namespace Rentering.Contracts.ApplicationEF.Handlers
{
    public class EstateContractHandlers : Notifiable,
        IHandler<CreateEstateContractCommandEF>,
        IHandler<CreatePaymentCycleCommandEF>,
        IHandler<InviteParticipantCommandEF>,
        IHandler<ExecutePaymentCommandEF>,
        IHandler<AcceptPaymentCommandEF>,
        IHandler<RejectPaymentCommandEF>,
        IHandler<AcceptToPaticipateCommandEF>,
        IHandler<RejectToParticipateCommandEF>,
        IHandler<GetCurrentOwedAmountCommandEF>
    {
        private readonly IContractUnitOfWorkEF _contractUnitOfWorkEF;

        public EstateContractHandlers(
            IContractUnitOfWorkEF contractUnitOfWorkEF)
        {
            _contractUnitOfWorkEF = contractUnitOfWorkEF;
        }

        public ICommandResult Handle(CreateEstateContractCommandEF command)
        {
            var contractName = command.ContractName;
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(command.PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(command.RentPrice);
            var rentDueDate = command.RentDueDate;
            var contractStartDate = command.ContractStartDate;
            var contractEndDate = command.ContractEndDate;

            var contractEntity = new EstateContractEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contractEntity.InviteParticipant(command.AccountId, e_ParticipantRole.Owner);

            if (_contractUnitOfWorkEF.EstateContractCUDRepositoryEF.ContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(address.Notifications);
            AddNotifications(propertyRegistrationNumber.Notifications);
            AddNotifications(rentPrice.Notifications);
            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.Add(contractEntity);
            _contractUnitOfWorkEF.Save();

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contractEntity.ContractName,
                contractEntity.RentPrice.Price
            });

            return createdContract;
        }

        public ICommandResult Handle(CreatePaymentCycleCommandEF command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(InviteParticipantCommandEF command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(ExecutePaymentCommandEF command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(AcceptPaymentCommandEF command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(RejectPaymentCommandEF command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(AcceptToPaticipateCommandEF command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(RejectToParticipateCommandEF command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(GetCurrentOwedAmountCommandEF command)
        {
            throw new NotImplementedException();
        }
    }
}
