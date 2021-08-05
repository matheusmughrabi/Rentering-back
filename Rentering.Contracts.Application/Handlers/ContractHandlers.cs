using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System.Collections.Generic;
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
        IHandler<GetCurrentOwedAmountCommand>,
        IHandler<ActivateContractCommand>
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
                return new CommandResult(false, "Erro ao criar contrato", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.ContractCUDRepository.Add(contractEntity);
            _contractUnitOfWork.Save();

            var createdContract = new CommandResult(true, "Contrato criado com sucesso!", null, new
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
            {
                AddNotification("Contrato", "Contrato não foi encontrado");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            if (newParticipantAccountId == 0)
            {
                AddNotification("Email", "Não foi encontrado um usuário com este email.");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }              

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
            {
                AddNotification("Autorização negada", "Apenas o criador do perfil pode convidar participantes");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            contractEntity?.InviteParticipant(newParticipantAccountId, (e_ParticipantRole)command.ParticipantRole);

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var updatedContract = new CommandResult(true, "Participante convidado com sucesso!", null, new
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
            {
                AddNotification("Contrato", "Contrato não encontrado");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
            {
                AddNotification("Autorização", "Apenas o do contrato pode remover participantes.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            if (command.AccountId == command.CurrentUserId)
            {
                AddNotification("Autorização", "Você é o criador do contrato e não pode ser removido.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            contractEntity?.RemoveParticipant(command.AccountId);

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var updatedContract = new CommandResult(true, "Participante removido com sucesso.", null, new
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
            {
                AddNotification("Contrato", "Contrato não encontrado.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserTheContractPayer = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Payer && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractPayer.Count() == 0)
            {
                AddNotification("Ação negada.", "Você não é pagador deste contrato.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var rejectedPaymentEntity = contractEntity.ExecutePayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var rejectedPayment = new CommandResult(true, "Pagamento realizado com sucesso!", null, null);

            return rejectedPayment;
        }
        #endregion

        #region Accept Payment
        public ICommandResult Handle(AcceptPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
            {
                AddNotification("Contrato", "Contrato não encontrado.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Receiver && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
            {
                AddNotification("Ação negada", "Apenas o locador do contrato pode aceitar pagamentos.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var acceptedPaymentEntity = contractEntity.AcceptPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(acceptedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var acceptedPayment = new CommandResult(true, "Pagamento aceito com sucesso!", null, null);

            return acceptedPayment;
        }
        #endregion

        #region Reject Payment
        public ICommandResult Handle(RejectPaymentCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
            {
                AddNotification("Contrato", "Contrato não encontrado.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Receiver && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
            {
                AddNotification("Ação negada", "Apenas o locador do contrato pode recusar pagamentos.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var acceptedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(acceptedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var acceptedPayment = new CommandResult(true, "Pagamento recusado com sucesso!", null, null);

            return acceptedPayment;
        }
        #endregion

        #region Accept to Participate
        public ICommandResult Handle(AcceptToParticipateCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
            {
                AddNotification("Contrato", "Contrato não encontrado.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            contractEntity.AcceptToParticipate(command.AccountContractId);

            AddNotifications(contractEntity);
            contractEntity.Participants.ToList().ForEach(c => AddNotifications(c.Notifications));

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var participant = new CommandResult(true, "Você aceitou participar do contrato com sucesso!", null, null);

            return participant;
        }
        #endregion

        #region Reject to Participate
        public ICommandResult Handle(RejectToParticipateCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            contractEntity.RejectToParticipate(command.AccountContractId);

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var participant = new CommandResult(false, "Você recusou participar do contrato!", null, null);

            return participant;
        }
        #endregion

        #region Activate Contract
        public ICommandResult Handle(ActivateContractCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
            {
                AddNotification("Contrato", "Contrato não encontrado");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
            {
                AddNotification("Contrato", "Somendo o criador do contrato pode ativá-lo.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            contractEntity.ActivateContract();

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _contractUnitOfWork.Save();

            var result = new CommandResult(false, "Corrija os erros abaixo.", null, null);

            return result;
        }
        #endregion

        #region Get Current Owed Amount
        public ICommandResult Handle(GetCurrentOwedAmountCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractCUDRepository.GetContractForCUD(command.ContractId);

            if (contractEntity == null)
            {
                AddNotification("Contrato", "Contrato não encontrado");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserParticipant = contractEntity.Participants.Any(c => c.AccountId == command.CurrentUserId && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserParticipant == false)
            {
                AddNotification("Acesso negado.", "Você não participa deste contrato.");
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);
            }

            if (contractEntity == null)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            var currentOwedAmount = contractEntity.CurrentOwedAmount();

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            var result = new CommandResult(true, "Valor devido calculador com sucesso!", null, new
            {
                CurrentOwedAmount = currentOwedAmount
            });

            return result;
        }
        #endregion
    }
}
