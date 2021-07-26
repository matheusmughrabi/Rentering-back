using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Corporation.Application.Commands;
using Rentering.Corporation.Domain.Data;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Corporation.Application.Handlers
{
    public class CorporationHandlers : Notifiable,
        IHandler<CreateCorporationCommand>
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
    }
}
