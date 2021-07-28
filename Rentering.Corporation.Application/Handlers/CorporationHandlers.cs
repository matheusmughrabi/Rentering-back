﻿using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Corporation.Application.Commands;
using Rentering.Corporation.Domain.Data;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Corporation.Application.Handlers
{
    public class CorporationHandlers : Notifiable,
        IHandler<CreateCorporationCommand>,
        IHandler<InviteToCorporationCommand>,
        IHandler<FinishCreationCommand>,
        IHandler<AcceptParticipationInCorporationCommand>,
        IHandler<RejectParticipationInCorporationCommand>
        
    {
        private readonly ICorporationUnitOfWork _corporationUnitOfWork;

        public CorporationHandlers(ICorporationUnitOfWork corporationUnitOfWork)
        {
            _corporationUnitOfWork = corporationUnitOfWork;
        }

        #region CreateCorporation
        public ICommandResult Handle(CreateCorporationCommand command)
        {
            var corporationEntity = new CorporationEntity(command.Name, command.CurrentUserId);

            if (Invalid)
                return new CommandResult(false, "Erro ao criar corporação", Notifications.ConvertCommandNotifications(), null);

            _corporationUnitOfWork.CorporationCUDRepository.Add(corporationEntity);
            _corporationUnitOfWork.Save();

            var result = new CommandResult(true, "Corporação criada com sucesso!", null, null);

            return result;
        }
        #endregion

        #region InviteToCorporation
        public ICommandResult Handle(InviteToCorporationCommand command)
        {
            var corporationEntity = _corporationUnitOfWork.CorporationCUDRepository.GetCorporationForCUD(command.ContractId);
            if (corporationEntity == null)
            {
                AddNotification("Corporação", "Corporação não foi encontrada");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserAdmin = corporationEntity.AdminId == command.CurrentUserId;
            if (isCurrentUserAdmin == false)
            {
                AddNotification("Autorização negada", "Apenas o administrador da corporação pode convidar participantes");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            var newParticipantAccountId = _corporationUnitOfWork.CorporationQueryRepository.GetAccountIdByEmail(command.Email);
            if (newParticipantAccountId == 0)
            {
                AddNotification("Email", "Não foi encontrado um usuário com este email.");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            corporationEntity.InviteParticipant(newParticipantAccountId, command.SharedPercentage);

            AddNotifications(corporationEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);

            _corporationUnitOfWork.Save();

            var result = new CommandResult(true, "Participante convidado com sucesso!", null, null);

            return result;
        }
        #endregion

        #region FinishCreation
        public ICommandResult Handle(FinishCreationCommand command)
        {
            var corporationEntity = _corporationUnitOfWork.CorporationCUDRepository.GetCorporationForCUD(command.CorporationId);
            if (corporationEntity == null)
            {
                AddNotification("Corporação", "Corporação não foi encontrada");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            var isCurrentUserAdmin = corporationEntity.AdminId == command.CurrentUserId;
            if (isCurrentUserAdmin == false)
            {
                AddNotification("Autorização negada", "Apenas o administrador da corporação pode convidar participantes");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            corporationEntity.FinishCreation();

            AddNotifications(corporationEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _corporationUnitOfWork.Save();

            var result = new CommandResult(true, "Você finalizou a etapa de criação da corporação!", null, null);

            return result;
        }
        #endregion

        #region AcceptParticipation
        public ICommandResult Handle(AcceptParticipationInCorporationCommand command)
        {
            var corporationEntity = _corporationUnitOfWork.CorporationCUDRepository.GetCorporationForCUD(command.CorporationId);
            if (corporationEntity == null)
            {
                AddNotification("Corporação", "Corporação não foi encontrada");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            corporationEntity.AcceptToParticipate(command.ParticipantId);

            AddNotifications(corporationEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _corporationUnitOfWork.Save();

            var participant = new CommandResult(true, "Você aceitou participar da corporação com sucesso!", null, null);

            return participant;
        }
        #endregion

        #region RejectParticipation
        public ICommandResult Handle(RejectParticipationInCorporationCommand command)
        {
            var corporationEntity = _corporationUnitOfWork.CorporationCUDRepository.GetCorporationForCUD(command.CorporationId);
            if (corporationEntity == null)
            {
                AddNotification("Corporação", "Corporação não foi encontrada");
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);
            }

            corporationEntity.RejectToParticipate(command.ParticipantId);

            AddNotifications(corporationEntity);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _corporationUnitOfWork.Save();

            var participant = new CommandResult(true, "Você aceitou participar da corporação com sucesso!", null, null);

            return participant;
        }
        #endregion
    }
}
