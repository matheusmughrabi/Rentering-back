using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class ContractUserProfileHandlers : Notifiable,
        ICommandHandler<CreateContractUserProfileCommand>
    {
        private readonly IContractUserProfileCUDRepository _userRepository;

        public ContractUserProfileHandlers(IContractUserProfileCUDRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(CreateContractUserProfileCommand command)
        {
            var userEntity = new ContractUserProfileEntity(command.AccountId);

            if (_userRepository.CheckIfAccountExists(command.AccountId) == false)
                AddNotification("AccountId", "This Account does not exist");

            AddNotifications(userEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _userRepository.CreateContractUserProfile(userEntity);

            var createdContractProfileUser = new CommandResult(true, "Profile created successfuly", new
            {
                command.AccountId
            });

            return createdContractProfileUser;
        }
    }
}
