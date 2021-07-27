using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Corporation.Application.Commands;
using Rentering.Corporation.Domain.Data;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Corporation.Application.Handlers
{
    public class CorporationHandlers : Notifiable,
        IHandler<CreateCorporationCommand>,
        IHandler<InviteToCorporationCommand>
    {
        private readonly ICorporationUnitOfWork _corporationUnitOfWork;

        public CorporationHandlers(ICorporationUnitOfWork corporationUnitOfWork)
        {
            _corporationUnitOfWork = corporationUnitOfWork;
        }

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

            corporationEntity.InviteParticipant(command.AccountId, command.SharedPercentage);

            AddNotifications(corporationEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Erro ao convidar participante.", Notifications.ConvertCommandNotifications(), null);

            _corporationUnitOfWork.Save();

            var result = new CommandResult(true, "Participante convidado com sucesso!", null, null);

            return result;
        }
    }
}
