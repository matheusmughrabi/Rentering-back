using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Extensions;
using Rentering.Contracts.Domain.ValueObjects;
using System.Linq;

namespace Rentering.Contracts.Application.Handlers
{
    public class ContractGuarantorHandlers : Notifiable,
        IHandler<CreateContractGuarantorCommand>,
        IHandler<InviteRenterToParticipate>,
        IHandler<InviteTenantToParticipate>,
        IHandler<InviteGuarantorToParticipate>,
        IHandler<CreateContractPaymentCycleCommand>,
        IHandler<ExecutePaymentCommand>,
        IHandler<AcceptPaymentCommand>,
        IHandler<RejectPaymentCommand>
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public ContractGuarantorHandlers(
            IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
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

            if (_contractUnitOfWork.ContractWithGuarantorQuery.CheckIfContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(address.Notifications);
            AddNotifications(propertyRegistrationNumber.Notifications);
            AddNotifications(rentPrice.Notifications);
            AddNotifications(contract.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.ContractWithGuarantorCUD.Create(contract);

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contract.ContractName,
                contract.RentPrice.Price
            });

            return createdContract;
        }

        public ICommandResult Handle(InviteRenterToParticipate command)
        {
            var contractFromDb = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(command.Id);
            var renterFromDb = _contractUnitOfWork.RenterQuery.GetById(command.RenterId);

            if (contractFromDb == null || renterFromDb == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract or renter not found" });

            var contractEntity = contractFromDb.EntityFromModel();
            var renterEntity = renterFromDb.EntityFromModel();

            contractEntity.InviteRenter(renterEntity);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(renterEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.BeginTransaction();
            _contractUnitOfWork.ContractWithGuarantorCUD.Update(command.Id, contractEntity);
            _contractUnitOfWork.RenterCUD.Update(command.RenterId, renterEntity);
            _contractUnitOfWork.Commit();

            var updatedContract = new CommandResult(true, "Renter invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }

        public ICommandResult Handle(InviteTenantToParticipate command)
        {
            var contractFromDb = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(command.Id);
            var tenantFromDb = _contractUnitOfWork.TenantQuery.GetById(command.TenantId);

            if (contractFromDb == null || tenantFromDb == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract or tenant not found" });

            var contractEntity = contractFromDb.EntityFromModel();
            var tenantEntity = tenantFromDb.EntityFromModel();

            contractEntity.InviteTenant(tenantEntity);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(tenantEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.ContractWithGuarantorCUD.Update(command.Id, contractEntity);
            _contractUnitOfWork.TenantCUD.Update(command.TenantId, tenantEntity);

            var updatedContract = new CommandResult(true, "Tenant invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }

        public ICommandResult Handle(InviteGuarantorToParticipate command)
        {
            var contractFromDb = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(command.Id);
            var guarantorFromDb = _contractUnitOfWork.GuarantorQuery.GetById(command.GuarantorId);

            if (contractFromDb == null || guarantorFromDb == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract or guarantor not found" });

            var contractEntity = contractFromDb.EntityFromModel();
            var guarantorEntity = guarantorFromDb.EntityFromModel();

            contractEntity.InviteGuarantor(guarantorEntity);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(guarantorEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWork.ContractWithGuarantorCUD.Update(command.Id, contractEntity);
            _contractUnitOfWork.GuarantorCUD.Update(command.GuarantorId, guarantorEntity);

            var updatedContract = new CommandResult(true, "Guarantor invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }

        public ICommandResult Handle(CreateContractPaymentCycleCommand command)
        {
            var contractEntity = _contractUnitOfWork.ContractWithGuarantorCUD.GetContractForCUD(command.ContractId);

            //var contractFromDb = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(command.ContractId);
            //var paymentsFromDb = _contractUnitOfWork.ContractPaymentQuery.GetPaymentsFromContract(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            //var contractEntity = contractFromDb.EntityFromModel();
            //var paymentEntities = paymentsFromDb?.Select(c => c.EntityFromModel()).ToList();

            //contractEntity.IncludeContractPayments(paymentEntities);

            contractEntity.CreatePaymentCycle();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            foreach (var payment in contractEntity.Payments)
            {
                _contractUnitOfWork.ContractPaymentCUD.Create(payment);
            }

            var createdPayments = new CommandResult(true, "Payment cycle created successfuly", new
            {
                contractEntity.Payments
            });

            return createdPayments;
        }

        public ICommandResult Handle(ExecutePaymentCommand command)
        {
            var contractFromDb = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(command.ContractId);
            var paymentsFromDb = _contractUnitOfWork.ContractPaymentQuery.GetPaymentsFromContract(command.ContractId);

            if (contractFromDb == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var contractEntity = contractFromDb.EntityFromModel();
            var paymentEntities = paymentsFromDb?.Select(c => c.EntityFromModel()).ToList();

            contractEntity.IncludeContractPayments(paymentEntities);
            contractEntity.ExecutePayment(command.Month);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var executedPaymentEntity = paymentEntities.Where(c => c.Month.ToShortDateString() == command.Month.ToShortDateString()).FirstOrDefault();

            _contractUnitOfWork.ContractPaymentCUD.Update(executedPaymentEntity.Id, executedPaymentEntity);

            var executedPayment = new CommandResult(true, "Payment executed successfuly", new
            {
                executedPaymentEntity
            });

            return executedPayment;
        }

        public ICommandResult Handle(AcceptPaymentCommand command)
        {
            var contractFromDb = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(command.ContractId);
            var paymentsFromDb = _contractUnitOfWork.ContractPaymentQuery.GetPaymentsFromContract(command.ContractId);

            if (contractFromDb == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var contractEntity = contractFromDb.EntityFromModel();
            var paymentEntities = paymentsFromDb?.Select(c => c.EntityFromModel()).ToList();

            contractEntity.IncludeContractPayments(paymentEntities);
            contractEntity.AcceptPayment(command.Month);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var acceptedPaymentEntity = paymentEntities.Where(c => c.Month.ToShortDateString() == command.Month.ToShortDateString()).FirstOrDefault();

            _contractUnitOfWork.ContractPaymentCUD.Update(acceptedPaymentEntity.Id, acceptedPaymentEntity);

            var acceptedPayment = new CommandResult(true, "Payment accepted successfuly", new
            {
                acceptedPaymentEntity
            });

            return acceptedPayment;
        }

        public ICommandResult Handle(RejectPaymentCommand command)
        {
            var contractFromDb = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(command.ContractId);
            var paymentsFromDb = _contractUnitOfWork.ContractPaymentQuery.GetPaymentsFromContract(command.ContractId);

            if (contractFromDb == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var contractEntity = contractFromDb.EntityFromModel();
            var paymentEntities = paymentsFromDb?.Select(c => c.EntityFromModel()).ToList();

            contractEntity.IncludeContractPayments(paymentEntities);
            contractEntity.RejectPayment(command.Month);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var rejectedPaymentEntity = paymentEntities.Where(c => c.Month.ToShortDateString() == command.Month.ToShortDateString()).FirstOrDefault();

            _contractUnitOfWork.ContractPaymentCUD.Update(rejectedPaymentEntity.Id, rejectedPaymentEntity);

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly", new
            {
                rejectedPaymentEntity
            });

            return rejectedPayment;
        }
    }
}
