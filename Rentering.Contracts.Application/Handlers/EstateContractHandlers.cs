using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System.Linq;

namespace Rentering.Contracts.Application.Handlers
{
    public class EstateContractHandlers : Notifiable,
        IHandler<CreateEstateContractCommand>,
        IHandler<CreatePaymentCycleCommand>,
        IHandler<InviteParticipantCommand>,
        IHandler<RemoveParticipantCommand>,
        IHandler<ExecutePaymentCommand>,
        IHandler<AcceptPaymentCommand>,
        IHandler<RejectPaymentCommand>,
        IHandler<AcceptToParticipateCommand>,
        IHandler<RejectToParticipateCommand>,
        IHandler<GetCurrentOwedAmountCommand>
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public EstateContractHandlers(IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        #region Create Contract
        public ICommandResult Handle(CreateEstateContractCommand command)
        {
            var contractName = command.ContractName;
            var rentPrice = new PriceValueObject(command.RentPrice);
            var rentDueDate = command.RentDueDate;
            var contractStartDate = command.ContractStartDate;
            var contractEndDate = command.ContractEndDate;

            var contractEntity = new ContractEntity(contractName, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contractEntity?.InviteParticipant(command.AccountId, e_ParticipantRole.Owner);

            if (_contractUnitOfWork.EstateContractCUDRepository.ContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(rentPrice.Notifications);
            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.EstateContractCUDRepository.Add(contractEntity);
            _contractUnitOfWork.Save();

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contractEntity.Id,
                contractEntity.ContractName,
                contractEntity.RentPrice.Price
            });

            return createdContract;
        }
        #endregion

        #region Invite Participant
        public ICommandResult Handle(InviteParticipantCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUDRepository.GetEstateContractForCUD(command.ContractId);
            var newParticipantAccountId = _contractUnitOfWork.EstateContractQueryRepository.GetAccountIdByEmail(command.Email);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            if (newParticipantAccountId == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Este email não está vinculado a uma conta" });

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only contract owners are allowed to invite participants" });

            contractEntity?.InviteParticipant(newParticipantAccountId, (e_ParticipantRole)command.ParticipantRole);

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var updatedContract = new CommandResult(true, "Participant invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }
        #endregion

        #region Remove Participant
        public ICommandResult Handle(RemoveParticipantCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUDRepository.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only contract owners are allowed to invite participants" });

            if (command.AccountId == command.CurrentUserId)
                return new CommandResult(false, "Fix erros below", new { Message = "Você é o criador deste contrato e não pode ser removido." });

            contractEntity?.RemoveParticipant(command.AccountId);

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var updatedContract = new CommandResult(true, "Participant invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }
        #endregion

        #region Create Payment Cycle
        public ICommandResult Handle(CreatePaymentCycleCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUDRepository.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only contract owners are allowed to create payment cycles" });

            contractEntity.CreatePaymentCycle();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var createdPayments = new CommandResult(true, "Payment cycle created successfuly", new
            {
                contractEntity.Payments
            });

            return createdPayments;
        }
        #endregion

        #region Execute Payment
        public ICommandResult Handle(ExecutePaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUDRepository.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractTenant = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Tenant && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractTenant.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only the contract tenants are allowed to execute payments" });

            var rejectedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly");

            return rejectedPayment;
        }
        #endregion

        #region Accept Payment
        public ICommandResult Handle(AcceptPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUDRepository.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Renter && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only the contract renters are allowed to accept payments" });

            var acceptedPaymentEntity = contractEntity.AcceptPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(acceptedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var acceptedPayment = new CommandResult(true, "Payment accepted successfuly");

            return acceptedPayment;
        }
        #endregion

        #region Reject Payment
        public ICommandResult Handle(RejectPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUDRepository.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Renter && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only the contract renters are allowed to reject payments" });

            var rejectedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly");

            return rejectedPayment;
        }
        #endregion

        #region Accept to Participate
        public ICommandResult Handle(AcceptToParticipateCommand command)
        {
            var participantForCUD = _contractUnitOfWork.AccountContractCUDRepository.GetAccountContractForCUD(command.AccountId, command.ContractId);

            if (participantForCUD == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Participant not found" });

            participantForCUD.AcceptToParticipate();

            AddNotifications(participantForCUD);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var participant = new CommandResult(true, "You have accepted to participate in the contract successfuly", new
            {
            });

            return participant;
        }
        #endregion

        #region Reject to Participate
        public ICommandResult Handle(RejectToParticipateCommand command)
        {
            var participantForCUD = _contractUnitOfWork.AccountContractCUDRepository.GetAccountContractForCUD(command.AccountId, command.ContractId);

            if (participantForCUD == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Participant not found" });

            participantForCUD.RejectToParticipate();

            AddNotifications(participantForCUD);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.Save();

            var participant = new CommandResult(true, "You have rejected to participate in the contract successfuly", new
            {
            });

            return participant;
        }
        #endregion

        #region Get Current Owed Amount
        public ICommandResult Handle(GetCurrentOwedAmountCommand command)
        {
            var contractEntity = _contractUnitOfWork.EstateContractCUDRepository.GetEstateContractForCUD(command.ContractId);

            var isCurrentUserParticipant = contractEntity.Participants.Any(c => c.AccountId == command.CurrentUserId && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserParticipant == false)
                return new CommandResult(false, "Fix erros below", new { Message = "You are not a participant of this contract" });

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
        #endregion
    }
}
