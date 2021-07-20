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
    public class ContractHandlers : Notifiable,
        IHandler<CreateContractCommand>,
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

        public ContractHandlers(IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        #region Create Contract
        public ICommandResult Handle(CreateContractCommand command)
        {
            var contractName = command.ContractName;
            var rentPrice = new PriceValueObject(command.RentPrice);
            var rentDueDate = command.RentDueDate;
            var contractStartDate = command.ContractStartDate;
            var contractEndDate = command.ContractEndDate;

            var contractEntity = new ContractEntity(contractName, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contractEntity?.InviteParticipant(command.AccountId, e_ParticipantRole.Owner);

            if (_contractUnitOfWork.ContractCUDRepository.ContractNameExists(command.ContractName))
                AddNotification("ContractName", "Este nome de contrato já existe. Por favor, tente um nome diferente.");

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.ContractCUDRepository.Add(contractEntity);
            _contractUnitOfWork.Save();

            var createdContract = new CommandResult(true, "Contrato criado com sucesso!", new
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
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);
            var newParticipantAccountId = _contractUnitOfWork.ContractQueryRepository.GetAccountIdByEmail(command.Email);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Contrato não encontrado." });

            if (newParticipantAccountId == 0)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Este email não está vinculado a uma conta" });

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Apenas o criador do contrato pode convidar participantes." });

            contractEntity?.InviteParticipant(newParticipantAccountId, (e_ParticipantRole)command.ParticipantRole);

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.Save();

            var updatedContract = new CommandResult(true, "Participante convidado com sucesso!", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }
        #endregion

        #region Remove Participant
        public ICommandResult Handle(RemoveParticipantCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Contracto não encontrado" });

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Apenas o do contrato pode remover participantes." });

            if (command.AccountId == command.CurrentUserId)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Você é o criador deste contrato e não pode ser removido." });

            contractEntity?.RemoveParticipant(command.AccountId);

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.Save();

            var updatedContract = new CommandResult(true, "Participante removido com sucesso.", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }
        #endregion

        #region Execute Payment
        public ICommandResult Handle(ExecutePaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Contrato não encontrado" });

            var isCurrentUserTheContractTenant = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Tenant && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractTenant.Count() == 0)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Apenas locatários do contrato podem realizar pagamentos." });

            var rejectedPaymentEntity = contractEntity.ExecutePayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.Save();

            var rejectedPayment = new CommandResult(true, "Pagamento realizado com sucesso!");

            return rejectedPayment;
        }
        #endregion

        #region Accept Payment
        public ICommandResult Handle(AcceptPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Contrato não encontrado" });

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Renter && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Apenas os locadores do contrato podem aceitar pagamentos." });

            var acceptedPaymentEntity = contractEntity.AcceptPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(acceptedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.Save();

            var acceptedPayment = new CommandResult(true, "Payment accepted successfuly");

            return acceptedPayment;
        }
        #endregion

        #region Reject Payment
        public ICommandResult Handle(RejectPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Contrato não foi encontrado" });

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Renter && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Apenas locadores do contrato podem recusar pagamentos." });

            var rejectedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.Save();

            var rejectedPayment = new CommandResult(true, "Pagamento recusado com sucesso!");

            return rejectedPayment;
        }
        #endregion

        #region Accept to Participate
        public ICommandResult Handle(AcceptToParticipateCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "A conta informada não foi encontrada." });

            contractEntity.AcceptToParticipate(command.AccountId);

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.Save();

            var participant = new CommandResult(true, "Você aceitou participar do contrato com sucesso!", new
            {
            });

            return participant;
        }
        #endregion

        #region Reject to Participate
        public ICommandResult Handle(RejectToParticipateCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "A conta informada não foi encontrada." });

            contractEntity.RejectToParticipate(command.AccountId);

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _contractUnitOfWork.Save();

            var participant = new CommandResult(true, "Você recusou participar do contrato com sucesso!", new
            {
            });

            return participant;
        }
        #endregion

        #region Get Current Owed Amount
        public ICommandResult Handle(GetCurrentOwedAmountCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            var isCurrentUserParticipant = contractEntity.Participants.Any(c => c.AccountId == command.CurrentUserId && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserParticipant == false)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Você não é um participate deste contrato." });

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Message = "Contrato não encontrado." });

            var currentOwedAmount = contractEntity.CurrentOwedAmount();

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            var result = new CommandResult(true, "Valor devido calculador com sucesso!", new
            {
                CurrentOwedAmount = currentOwedAmount
            });

            return result;
        }
        #endregion
    }
}
