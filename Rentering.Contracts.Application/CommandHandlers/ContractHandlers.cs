using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class ContractHandlers : Notifiable,
        ICommandHandler<CreateContractCommand>,
        ICommandHandler<UpdateRentPriceCommand>,
        ICommandHandler<DeleteContractCommand>
    {
        private readonly IContractCUDRepository _contractRepository;

        public ContractHandlers(IContractCUDRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public ICommandResult Handle(CreateContractCommand command)
        {
            var contractName = command.ContractName;
            var rentPrice = new PriceValueObject(command.Price);
            var renterId = command.RenterId;
            var tenantId = command.TentantId;
            var contract = new ContractEntity(contractName, rentPrice, renterId, tenantId);

            if (_contractRepository.CheckIfContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(rentPrice.Notifications);
            AddNotifications(contract.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractRepository.CreateContract(contract);

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contract.ContractName,
                contract.RentPrice.Price,
                contract.RenterId,
                contract.TentantId
            });

            return createdContract;
        }

        public ICommandResult Handle(UpdateRentPriceCommand command)
        {
            var contractEntityFromDb = _contractRepository.GetContractById(command.Id);

            var rentPrice = new PriceValueObject(command.Price);
            contractEntityFromDb.UpdateRentPrice(rentPrice);

            AddNotifications(contractEntityFromDb.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractRepository.UpdateContractRentPrice(command.Id, contractEntityFromDb);

            var updatedContractRentPrice = new CommandResult(true, "Contract rent updated successfuly", new
            {
                contractEntityFromDb.ContractName,
                contractEntityFromDb.RentPrice.Price
            });

            return updatedContractRentPrice;
        }

        public ICommandResult Handle(DeleteContractCommand command)
        {
            _contractRepository.DeleteContract(command.Id);

            var deletedContract = new CommandResult(true, "Contract deleted successfuly", new
            {
                ContractId = command.Id
            });

            return deletedContract;
        }
    }
}
