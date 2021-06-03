using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.ApplicationEF.Commands;
using Rentering.Contracts.Domain.DataEF;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Linq;

namespace Rentering.Contracts.ApplicationEF.Handlers
{
    public class EstateContractHandlers : Notifiable,
        IHandler<CreateEstateContractCommandEF>,
        IHandler<AddRenterToContractCommandEF>,
        IHandler<AddTenantToContractCommandEF>,
        IHandler<AddGuarantorToContractCommandEF>,
        IHandler<CreatePaymentCycleCommandEF>,
        IHandler<InviteParticipantCommandEF>,
        IHandler<ExecutePaymentCommandEF>,
        IHandler<AcceptPaymentCommandEF>,
        IHandler<RejectPaymentCommandEF>,
        IHandler<AcceptToParticipateCommandEF>,
        IHandler<RejectToParticipateCommandEF>,
        IHandler<GetCurrentOwedAmountCommandEF>
    {
        private readonly IContractUnitOfWorkEF _contractUnitOfWorkEF;

        public EstateContractHandlers(
            IContractUnitOfWorkEF contractUnitOfWorkEF)
        {
            _contractUnitOfWorkEF = contractUnitOfWorkEF;
        }

        public ICommandResult Handle(CreateEstateContractCommandEF command)
        {
            var contractName = command.ContractName;
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(command.PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(command.RentPrice);
            var rentDueDate = command.RentDueDate;
            var contractStartDate = command.ContractStartDate;
            var contractEndDate = command.ContractEndDate;

            var contractEntity = new EstateContractEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contractEntity?.InviteParticipant(command.AccountId, e_ParticipantRole.Owner);

            if (_contractUnitOfWorkEF.EstateContractCUDRepositoryEF.ContractNameExists(command.ContractName))
                AddNotification("ContractName", "This ContractName is already registered");

            AddNotifications(address.Notifications);
            AddNotifications(propertyRegistrationNumber.Notifications);
            AddNotifications(rentPrice.Notifications);
            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.Add(contractEntity);
            _contractUnitOfWorkEF.Save();

            var createdContract = new CommandResult(true, "Contract created successfuly", new
            {
                contractEntity.ContractName,
                contractEntity.RentPrice.Price
            });

            return createdContract;
        }

        public ICommandResult Handle(InviteParticipantCommandEF command)
        {
            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only contract owners are allowed to invite participants" });

            //if (_contractUnitOfWork.AccountContractsQuery.CheckIfAccountExists(command.ParticipantAccountId) == false)
            //    return new CommandResult(false, "Fix erros below", new { Message = "Account not found" });

            contractEntity?.InviteParticipant(command.ParticipantAccountId, command.ParticipantRole);
            //var invitedParticipant = contractEntity.Participants.Last();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var updatedContract = new CommandResult(true, "Participant invited successfuly", new
            {
                contractEntity.ContractName
            });

            return updatedContract;
        }

        public ICommandResult Handle(AddRenterToContractCommandEF command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var renterEntity = new RenterEntity(command.ContractId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG, cpf, address, spouseName, command.SpouseNationality, spouseIdentityRG, spouseCPF);

            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);
            contractEntity?.AddRenter(renterEntity);

            if (contractEntity == null)
                AddNotification("ContractId", "This Contract does not exist");

            AddNotifications(name.Notifications);
            AddNotifications(identityRG.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(spouseName.Notifications);
            AddNotifications(spouseIdentityRG.Notifications);
            AddNotifications(spouseCPF.Notifications);
            AddNotifications(renterEntity.Notifications);
            AddNotifications(contractEntity?.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var createdRenter = new CommandResult(true, "Renter created successfuly", new
            {
                command.ContractId,
                command.FirstName,
                command.LastName,
                command.Nationality,
                command.Ocupation,
                command.MaritalStatus,
                command.IdentityRG,
                command.CPF,
                command.Street,
                command.Neighborhood,
                command.City,
                command.CEP,
                command.State,
                command.SpouseFirstName,
                command.SpouseLastName,
                command.SpouseNationality,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return createdRenter;
        }

        public ICommandResult Handle(AddTenantToContractCommandEF command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var tenantEntity = new TenantEntity(command.ContractId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG, cpf, address, spouseName, command.SpouseNationality, command.SpouseOcupation, spouseIdentityRG, spouseCPF);

            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);
            contractEntity?.AddTenant(tenantEntity);

            if (contractEntity == null)
                AddNotification("ContractId", "This Contract does not exist");

            AddNotifications(name.Notifications);
            AddNotifications(identityRG.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(spouseName.Notifications);
            AddNotifications(spouseIdentityRG.Notifications);
            AddNotifications(spouseCPF.Notifications);
            AddNotifications(tenantEntity.Notifications);
            AddNotifications(contractEntity?.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var createdTenant = new CommandResult(true, "Tenant created successfuly", new
            {
                command.ContractId,
                command.FirstName,
                command.LastName,
                command.Nationality,
                command.Ocupation,
                command.MaritalStatus,
                command.IdentityRG,
                command.CPF,
                command.Street,
                command.Neighborhood,
                command.City,
                command.CEP,
                command.State,
                command.SpouseFirstName,
                command.SpouseLastName,
                command.SpouseNationality,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return createdTenant;
        }

        public ICommandResult Handle(AddGuarantorToContractCommandEF command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var identityRG = new IdentityRGValueObject(command.IdentityRG);
            var cpf = new CPFValueObject(command.CPF);
            var address = new AddressValueObject(command.Street, command.Neighborhood, command.City, command.CEP, command.State);
            var spouseName = new NameValueObject(command.SpouseFirstName, command.SpouseLastName, false, false);
            var spouseIdentityRG = new IdentityRGValueObject(command.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(command.SpouseCPF, false);

            var guarantorEntity = new GuarantorEntity(command.ContractId, name, command.Nationality, command.Ocupation, command.MaritalStatus, identityRG, cpf, address, spouseName, command.SpouseNationality, command.SpouseOcupation, spouseIdentityRG, spouseCPF);

            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);
            contractEntity?.AddGuarantor(guarantorEntity);

            if (contractEntity == null)
                AddNotification("ContractId", "This Contract does not exist");

            AddNotifications(name.Notifications);
            AddNotifications(identityRG.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(spouseName.Notifications);
            AddNotifications(spouseIdentityRG.Notifications);
            AddNotifications(spouseCPF.Notifications);
            AddNotifications(guarantorEntity.Notifications);
            AddNotifications(contractEntity?.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var createdGuarantor = new CommandResult(true, "Guarantor created successfuly", new
            {
                command.ContractId,
                command.FirstName,
                command.LastName,
                command.Nationality,
                command.Ocupation,
                command.MaritalStatus,
                command.IdentityRG,
                command.CPF,
                command.Street,
                command.Neighborhood,
                command.City,
                command.CEP,
                command.State,
                command.SpouseFirstName,
                command.SpouseLastName,
                command.SpouseNationality,
                command.SpouseIdentityRG,
                command.SpouseCPF
            });

            return createdGuarantor;
        }

        public ICommandResult Handle(CreatePaymentCycleCommandEF command)
        {
            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractOwner = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Owner && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractOwner.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only contract owners are allowed to create payment cycles" });

            contractEntity.CreatePaymentCycle();

            AddNotifications(contractEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var createdPayments = new CommandResult(true, "Payment cycle created successfuly", new
            {
                contractEntity.Payments
            });

            return createdPayments;
        }

        public ICommandResult Handle(ExecutePaymentCommandEF command)
        {
            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractTenant = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Tenant && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractTenant.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only the contract tenants are allowed to execute payments" });

            var rejectedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly");

            return rejectedPayment;
        }

        public ICommandResult Handle(AcceptPaymentCommandEF command)
        {
            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Renter && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only the contract renters are allowed to accept payments" });

            var acceptedPaymentEntity = contractEntity.AcceptPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(acceptedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var acceptedPayment = new CommandResult(true, "Payment accepted successfuly");

            return acceptedPayment;
        }

        public ICommandResult Handle(RejectPaymentCommandEF command)
        {
            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var isCurrentUserTheContractRenter = contractEntity.Participants
                .Where(c => c.AccountId == command.CurrentUserId && c.ParticipantRole == e_ParticipantRole.Renter && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserTheContractRenter.Count() == 0)
                return new CommandResult(false, "Fix erros below", new { Message = "Only the contract renters are allowed to reject payments" });

            var rejectedPaymentEntity = contractEntity.RejectPayment(command.Month);

            AddNotifications(contractEntity.Notifications);
            AddNotifications(rejectedPaymentEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var rejectedPayment = new CommandResult(true, "Payment rejected successfuly");

            return rejectedPayment;
        }

        public ICommandResult Handle(AcceptToParticipateCommandEF command)
        {
            var participantForCUD = _contractUnitOfWorkEF.AccountContractCUDRepositoryEF.GetAccountContractForCUD(command.AccountId, command.ContractId);

            if (participantForCUD == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Participant not found" });

            participantForCUD.AcceptToParticipate();

            AddNotifications(participantForCUD);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var participant = new CommandResult(true, "You have accepted to participate in the contract successfuly", new
            {
            });

            return participant;
        }

        public ICommandResult Handle(RejectToParticipateCommandEF command)
        {
            var participantForCUD = _contractUnitOfWorkEF.AccountContractCUDRepositoryEF.GetAccountContractForCUD(command.AccountId, command.ContractId);

            if (participantForCUD == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Participant not found" });

            participantForCUD.RejectToParticipate();

            AddNotifications(participantForCUD);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractUnitOfWorkEF.Save();

            var participant = new CommandResult(true, "You have rejected to participate in the contract successfuly", new
            {
            });

            return participant;
        }

        public ICommandResult Handle(GetCurrentOwedAmountCommandEF command)
        {
            var contractEntity = _contractUnitOfWorkEF.EstateContractCUDRepositoryEF.GetEstateContractForCUD(command.ContractId);

            var isCurrentUserParticipant = contractEntity.Participants.Any(c => c.AccountId == command.CurrentUserId && c.Status == e_ParticipantStatus.Accepted);

            if (isCurrentUserParticipant == false)
                return new CommandResult(false, "Fix erros below", new { Message = "You are not a participant of this contract" });

            if (contractEntity == null)
                return new CommandResult(false, "Fix erros below", new { Message = "Contract not found" });

            var currentOwedAmount = contractEntity.CurrentOwedAmount();

            AddNotifications(contractEntity);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var result = new CommandResult(true, "Current owed amount calculated successfuly", new
            {
                CurrentOwedAmount = currentOwedAmount
            });

            return result;
        }
    }
}
