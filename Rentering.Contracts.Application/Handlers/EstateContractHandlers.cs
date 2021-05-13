using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;
using System.Linq;

namespace Rentering.Contracts.Application.Handlers
{
    public class EstateContractHandlers : Notifiable,
        IHandler<CreateEstateContractCommand>,
        IHandler<CreateContractPaymentCycleCommand>,
        IHandler<InviteParticipantCommand>,
        IHandler<ExecutePaymentCommand>,
        IHandler<AcceptPaymentCommand>,
        IHandler<RejectPaymentCommand>
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public EstateContractHandlers(
            IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        public ICommandResult Handle(CreateEstateContractCommand command)
        {
            var contractName = command.ContractName;
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(command.PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(command.RentPrice);
            var rentDueDate = command.RentDueDate;
            var contractStartDate = command.ContractStartDate;
            var contractEndDate = command.ContractEndDate;

            var contract = new EstateContractEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            if (_contractUnitOfWork.EstateContractQuery.CheckIfContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(address.Notifications);
            AddNotifications(propertyRegistrationNumber.Notifications);
            AddNotifications(rentPrice.Notifications);
            AddNotifications(contract.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.EstateContractCUD.Create(contract);

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contract.ContractName,
                contract.RentPrice.Price
            });

            return createdContract;
        }

        public ICommandResult Handle(InviteParticipantCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            contractEntity.InviteParticipant(command.AccountId, command.ParticipantRole);
            var invitedParticipant = contractEntity.Participants.Last();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.BeginTransaction();
            _contractUnitOfWork.EstateContractCUD.Update(command.ContractId, contractEntity);
            _contractUnitOfWork.AccountContractsCUD.Create(invitedParticipant);
            _contractUnitOfWork.Commit();

            var updatedContract = new CommandResult(true, "Participant invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }

        public ICommandResult Handle(CreateContractPaymentCycleCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            contractEntity.CreatePaymentCycle();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            foreach (var payment in contractEntity.Payments)
            {
                _contractUnitOfWork.ContractPaymentCUD.Create(payment);
            }

            var createdPayments = new CommandResult(true, "Payment cycle created successfuly", new
            {
                contractEntity.Payments
            });

            return createdPayments;
        }

        public ICommandResult Handle(ExecutePaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });
            contractEntity.ExecutePayment(command.Month);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var executedPaymentEntity = contractEntity.Payments
                .Where(c => c.Month.ToShortDateString() == command.Month.ToShortDateString()).FirstOrDefault();

            _contractUnitOfWork.ContractPaymentCUD.Update(executedPaymentEntity.Id, executedPaymentEntity);

            var executedPayment = new CommandResult(true, "Payment executed successfuly", new
            {
                executedPaymentEntity
            });

            return executedPayment;
        }

        public ICommandResult Handle(AcceptPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            contractEntity.AcceptPayment(command.Month);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var acceptedPaymentEntity = contractEntity.Payments
                .Where(c => c.Month.ToShortDateString() == command.Month.ToShortDateString()).FirstOrDefault();

            _contractUnitOfWork.ContractPaymentCUD.Update(acceptedPaymentEntity.Id, acceptedPaymentEntity);

            var acceptedPayment = new CommandResult(true, "Payment accepted successfuly", new
            {
                acceptedPaymentEntity
            });

            return acceptedPayment;
        }

        public ICommandResult Handle(RejectPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            contractEntity.RejectPayment(command.Month);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var rejectedPaymentEntity = contractEntity.Payments
                .Where(c => c.Month.ToShortDateString() == command.Month.ToShortDateString()).FirstOrDefault();

            _contractUnitOfWork.ContractPaymentCUD.Update(rejectedPaymentEntity.Id, rejectedPaymentEntity);

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly", new
            {
                rejectedPaymentEntity
            });

            return rejectedPayment;
        }
    }
}
