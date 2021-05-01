using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class EstateContractGuarantorHandlers : Notifiable,
        ICommandHandler<CreateEstateContractGuarantorCommand>
    {
        public ICommandResult Handle(CreateEstateContractGuarantorCommand command)
        {
            var contractName = command.ContractName;

            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(command.PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(command.RentPrice);
            var rentDueDate = command.RentDueDate;
            var contractStartDate = command.ContractStartDate;
            var contractEndDate = command.ContractEndDate;

            var contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            //if (_contractRepository.CheckIfContractNameExists(command.ContractName))
            //    AddNotification("ContractName", "This ContractName is already registered");

            // Check if renterId exists -> Must exist
            // Check if tenantId exists -> Must exist
            // Check if guarantorId exists -> Must exist

            AddNotifications(rentPrice.Notifications);
            AddNotifications(contract.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            //_contractRepository.CreateContract(contract);

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contract.ContractName,
                contract.RentPrice.Price,
            });

            return createdContract;
        }
    }
}
