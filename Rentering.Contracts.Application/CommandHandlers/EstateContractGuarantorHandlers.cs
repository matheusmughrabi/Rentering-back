using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.UtilRepositories;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class EstateContractGuarantorHandlers : Notifiable,
        ICommandHandler<CreateContractGuarantorCommand>,
        ICommandHandler<InviteRenterToParticipate>
    {
        private readonly IContractWithGuarantorCUDRepository _contractWithGuarantorCUDRepository;
        private readonly IContractWithGuarantorUtilRepository _contractWithGuarantorUtilRepository;

        public EstateContractGuarantorHandlers(IContractWithGuarantorCUDRepository contractWithGuarantorCUDRepository)
        {
            _contractWithGuarantorCUDRepository = contractWithGuarantorCUDRepository;
        }

        public EstateContractGuarantorHandlers(
            IContractWithGuarantorCUDRepository contractWithGuarantorCUDRepository, 
            IContractWithGuarantorUtilRepository contractWithGuarantorUtilRepository)
        {
            _contractWithGuarantorCUDRepository = contractWithGuarantorCUDRepository;
            _contractWithGuarantorUtilRepository = contractWithGuarantorUtilRepository;
        }

        public ICommandResult Handle(CreateContractGuarantorCommand command)
        {
            var contractName = command.ContractName;
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(command.PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(command.RentPrice);
            var rentDueDate = command.RentDueDate;
            var contractStartDate = command.ContractStartDate;
            var contractEndDate = command.ContractEndDate;

            var contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            if (_contractWithGuarantorUtilRepository.CheckIfContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(address.Notifications);
            AddNotifications(propertyRegistrationNumber.Notifications);
            AddNotifications(rentPrice.Notifications);
            AddNotifications(contract.Notifications);           

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractWithGuarantorCUDRepository.CreateContract(contract);

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contract.ContractName,
                contract.RentPrice.Price
            });

            return createdContract;
        }

        public ICommandResult Handle(InviteRenterToParticipate command)
        {
            throw new System.NotImplementedException();

            //var contractEntityFromDb = _contractWithGuarantorCUDRepository.GetContractById(command.Id);

            //contractEntityFromDb.InviteRenter();

            //AddNotifications(contractEntityFromDb.Notifications);

            //if (Invalid)
            //    return new CommandResult(false, "Fix erros below", new { Notifications });

            //_contractRepository.UpdateContractRentPrice(command.Id, contractEntityFromDb);

            //var updatedContractRentPrice = new CommandResult(true, "Contract rent updated successfuly", new
            //{
            //    contractEntityFromDb.ContractName,
            //    contractEntityFromDb.RentPrice.Price
            //});

            //return updatedContractRentPrice;

            // Buscar Contrato do BD
            // Chamar método InviteRenter
            // Persistir mudança no BD
        }
    }
}
