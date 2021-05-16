using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Linq;

namespace Rentering.Contracts.Application.Handlers
{
    public class EstateContractHandlers : Notifiable,
        IHandler<CreateEstateContractCommand>,
        IHandler<CreateContractPaymentCycleCommand>,
        IHandler<InviteParticipantCommand>,
        IHandler<ExecutePaymentCommand>,
        IHandler<AcceptPaymentCommand>,
        IHandler<RejectPaymentCommand>,
        IHandler<AcceptToParticipateCommand>,
        IHandler<RejectToParticipateCommand>,
        IHandler<GetCurrentOwedAmountCommand>
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

            var contractEntity = new EstateContractEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            if (_contractUnitOfWork.EstateContractQuery.CheckIfContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(address.Notifications);
            AddNotifications(propertyRegistrationNumber.Notifications);
            AddNotifications(rentPrice.Notifications);
            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            // TODO - Trabalhar a gambiarra aqui
            try
            {
                _contractUnitOfWork.BeginTransaction();
                var newContract = _contractUnitOfWork.EstateContractCUD.Create(contractEntity);

                var owner = e_ParticipantRole.Owner;
                newContract.InviteParticipant(command.AccountId, owner);
                var invitedParticipant = newContract.Participants.Last();

                _contractUnitOfWork.AccountContractsCUD.Create(invitedParticipant);
                _contractUnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _contractUnitOfWork.Rollback();
                return new CommandResult(false, "Internal Server Error", new { Error = ex.Message });
            }

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contractEntity.ContractName,
                contractEntity.RentPrice.Price
            });

            return createdContract;
        }

        public ICommandResult Handle(InviteParticipantCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            // TODO - CheckIfAccountExists

            contractEntity.InviteParticipant(command.AccountId, command.ParticipantRole);
            var invitedParticipant = contractEntity.Participants.Last();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            try
            {
                _contractUnitOfWork.BeginTransaction();
                _contractUnitOfWork.EstateContractCUD.Update(command.ContractId, contractEntity);
                _contractUnitOfWork.AccountContractsCUD.Create(invitedParticipant);
                _contractUnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _contractUnitOfWork.Rollback();
                return new CommandResult(false, "Internal Server Error", new { Error = ex.Message });
            }

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

            var rejectedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.ContractPaymentCUD.Update(rejectedPaymentEntity.Id, rejectedPaymentEntity);

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly");

            return rejectedPayment;
        }

        public ICommandResult Handle(AcceptPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var acceptedPaymentEntity = contractEntity.AcceptPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(acceptedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.ContractPaymentCUD.Update(acceptedPaymentEntity.Id, acceptedPaymentEntity);

            var acceptedPayment = new CommandResult(true, "Payment accepted successfuly");

            return acceptedPayment;
        }

        public ICommandResult Handle(RejectPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var rejectedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.ContractPaymentCUD.Update(rejectedPaymentEntity.Id, rejectedPaymentEntity);

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly");

            return rejectedPayment;
        }

        public ICommandResult Handle(AcceptToParticipateCommand command)
        {
            var participantForCUD = _contractUnitOfWork.AccountContractsCUD.GetParticipantForCUD(command.AccountId, command.ContractId);

            if (participantForCUD == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Participant not found" });

            participantForCUD.AcceptToParticipate();

            AddNotifications(participantForCUD);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.AccountContractsCUD.Update(participantForCUD.Id, participantForCUD);

            var participant = new CommandResult(true, "You have accepted to participate in the contract successfuly", new
            {
            });

            return participant;
        }

        public ICommandResult Handle(RejectToParticipateCommand command)
        {
            var participantForCUD = _contractUnitOfWork.AccountContractsCUD.GetParticipantForCUD(command.AccountId, command.ContractId);

            if (participantForCUD == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Participant not found" });

            participantForCUD.RejectToParticipate();

            AddNotifications(participantForCUD);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.AccountContractsCUD.Update(participantForCUD.Id, participantForCUD);

            var participant = new CommandResult(true, "You have rejected to participate in the contract successfuly", new
            {
            });

            return participant;
        }

        public ICommandResult Handle(GetCurrentOwedAmountCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUD.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var currentOwedAmount = contractEntity.CurrentOwedAmount();

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var result = new CommandResult(true, "Current owed amount calculated successfuly", new
            {
                CurrentOwedAmount = currentOwedAmount
            });

            return result;
        }
    }
}
