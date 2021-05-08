using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Extensions;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class ContractGuarantorHandlers : Notifiable,
        ICommandHandler<CreateContractGuarantorCommand>,
        ICommandHandler<InviteRenterToParticipate>,
        ICommandHandler<CreateContractPaymentCycleCommand>
    {
        private readonly IContractWithGuarantorCUDRepository _contractWithGuarantorCUDRepository;
        private readonly IContractWithGuarantorQueryRepository _contractWithGuarantorQueryRepository;
        private readonly IRenterCUDRepository _renterCUDRepository;
        private readonly IRenterQueryRepository _renterQueryRepository;
        private readonly ITenantCUDRepository _tenantCUDRepository;
        private readonly ITenantQueryRepository _tenantQueryRepository;

        private readonly IContractPaymentCUDRepository _contractPaymentCUDRepository;
        private readonly IContractPaymentQueryRepository _contractPaymentQueryRepository;

        public ContractGuarantorHandlers(
            IContractWithGuarantorCUDRepository contractWithGuarantorCUDRepository,
            IContractWithGuarantorQueryRepository contractWithGuarantorQueryRepository,
            IRenterCUDRepository renterCUDRepository,
            IRenterQueryRepository renterQueryRepository,
            ITenantCUDRepository tenantCUDRepository,
            ITenantQueryRepository tenantQueryRepository, 
            IContractPaymentCUDRepository contractPaymentCUDRepository, 
            IContractPaymentQueryRepository contractPaymentQueryRepository)
        {
            _contractWithGuarantorCUDRepository = contractWithGuarantorCUDRepository;
            _contractWithGuarantorQueryRepository = contractWithGuarantorQueryRepository;
            _renterCUDRepository = renterCUDRepository;
            _renterQueryRepository = renterQueryRepository;
            _tenantCUDRepository = tenantCUDRepository;
            _tenantQueryRepository = tenantQueryRepository;
            _contractPaymentCUDRepository = contractPaymentCUDRepository;
            _contractPaymentQueryRepository = contractPaymentQueryRepository;
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

            if (_contractWithGuarantorQueryRepository.CheckIfContractNameExists(command.ContractName))
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
            var contractFromDb = _contractWithGuarantorQueryRepository.GetContractById(command.Id);
            var contractEntity = contractFromDb.EntityFromModel();

            var renterFromDb = _renterQueryRepository.GetRenterById(command.RenterId);
            var renterEntity = renterFromDb.EntityFromModel();

            contractEntity.InviteRenter(renterEntity);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(renterEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractWithGuarantorCUDRepository.UpdateContract(command.Id, contractEntity);
            _renterCUDRepository.UpdateRenter(command.RenterId, renterEntity);

            var updatedContract = new CommandResult(true, "Renter invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }

        public ICommandResult Handle(InviteTenantToParticipate command)
        {
            var contractFromDb = _contractWithGuarantorQueryRepository.GetContractById(command.Id);
            var contractEntity = contractFromDb.EntityFromModel();

            var tenantFromDb = _tenantQueryRepository.GetTenantById(command.TenantId);
            var tenantEntity = tenantFromDb.EntityFromModel();

            contractEntity.InviteTenant(tenantEntity);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(tenantEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractWithGuarantorCUDRepository.UpdateContract(command.Id, contractEntity);
            _tenantCUDRepository.UpdateTenant(command.TenantId, tenantEntity);

            var updatedContract = new CommandResult(true, "Tenant invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }

        public ICommandResult Handle(CreateContractPaymentCycleCommand command)
        {
            var contractFromDb = _contractWithGuarantorQueryRepository.GetContractById(command.ContractId);
            var contractEntity = contractFromDb.EntityFromModel();

            contractEntity.CreatePaymentCycle();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            foreach (var payment in contractEntity.Payments)
            {
                _contractPaymentCUDRepository.CreatePayment(payment);
            }

            var createdPayments = new CommandResult(true, "Payment cycle created successfuly", new
            {
                contractEntity.Payments
            });

            return createdPayments;
        }
    }
}
