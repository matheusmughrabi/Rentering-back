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
            var rentPrice = new PriceValueObject(command.RentPrice);
            var renterId = command.RenterId;
            var tenantId = command.TenantId;
            var guarantorId = command.GuarantorId;
            var contract = new ContractWithGuarantorEntity(contractName, rentPrice, renterId, tenantId, guarantorId);

            //if (_contractRepository.CheckIfContractNameExists(command.ContractName))
            //    AddNotification("ContractName", "This ContractName is already registered");

            // Check if renterId exists -> Musts exist
            // Check if tenantId exists -> Musts exist
            // Check if guarantorId exists -> Musts exist

            AddNotifications(rentPrice.Notifications);
            AddNotifications(contract.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            //_contractRepository.CreateContract(contract);

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contract.ContractName,
                contract.RentPrice.Price,
                contract.RenterId,
                contract.TenantId,
                contract.GuarantorId
            });

            return createdContract;
        }
    }
}
