using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;

namespace Rentering.Contracts.Application.CommandHandlers
{
    public class ContractPaymentHandlers : Notifiable,
        ICommandHandler<CreatePaymentCycleCommand>,
        ICommandHandler<AcceptPaymentCommand>,
        ICommandHandler<ExecutePaymentCommand>,
        ICommandHandler<RejectPaymentCommand>
    {
        private readonly IContractPaymentCUDRepository _contractPaymentRepository;

        public ContractPaymentHandlers(IContractPaymentCUDRepository contractPaymentRepository)
        {
            _contractPaymentRepository = contractPaymentRepository;
        }

        public ICommandResult Handle(CreatePaymentCycleCommand command)
        {
            if (_contractPaymentRepository.CheckIfContractExists(command.ContractId) == false)
                AddNotification("Contract", "This contract does not exist");

            if (_contractPaymentRepository.CheckIfDateIsAlreadyRegistered(command.ContractId, command.Month) == true)
                AddNotification("Contract", "This date is already registered in a payment cycle for this contract");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            const int annualMonthSpan = 12;

            for (int i = 0; i < annualMonthSpan; i++)
            {
                var month = command.Month.AddMonths(i);
                var contractPaymentEntity = new ContractPaymentEntity(command.ContractId, month);
                _contractPaymentRepository.CreatePaymentAnnucalCycle(contractPaymentEntity);
            }

            var paymentCycleCommandResult = new CommandResult(true, "Payment cycle created successfuly", new
            {
                command.ContractId,
                StartDate = command.Month.ToShortDateString(),
                EndDate = command.Month.AddMonths(annualMonthSpan).ToShortDateString()
            });

            return paymentCycleCommandResult;
        }

        public ICommandResult Handle(AcceptPaymentCommand command)
        {
            // Validates if the logged user is the renter of the contract
            //var contractDataAuthorization = new ContractDataAuthorization(_contractAuthRepository);
            //bool isCurrentUserContractRenter = contractDataAuthorization.IsCurrentUserContractRenter(authenticatedUserId, command.ContractId);

            //if (isCurrentUserContractRenter == false)
            //    AddNotification("AuthenticatedUserId", "Current user is not the contract renter");

            //if (Invalid)
            //    return new CommandResult(false, "Fix erros below", new { Notifications });

            // Validates contract data
            if (_contractPaymentRepository.CheckIfContractExists(command.ContractId) == false)
                AddNotification("Contract", "This contract does not exist");

            else if (_contractPaymentRepository.CheckIfDateIsAlreadyRegistered(command.ContractId, command.Month) == false)
                AddNotification("Contract", "This date is not registered in a payment cycle for this contract");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var contractPaymentEntityFromDb = _contractPaymentRepository.GetContractPaymentByContractIdAndMonth(command.ContractId, command.Month);
            contractPaymentEntityFromDb.AcceptPayment();

            AddNotifications(contractPaymentEntityFromDb.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractPaymentRepository.AcceptPayment(contractPaymentEntityFromDb);

            var contractPaymentCommandResult = new CommandResult(true, "Payment accepted successfuly", new
            {
                contractPaymentEntityFromDb.ContractId,
                contractPaymentEntityFromDb.Month
            });

            return contractPaymentCommandResult;
        }

        public ICommandResult Handle(ExecutePaymentCommand command)
        {
            // Validates if the logged user is the renter of the contract
            //var contractDataAuthorization = new ContractDataAuthorization(_contractAuthRepository);
            //bool isCurrentUserContractRenter = contractDataAuthorization.IsCurrentUserContractTenant(authenticatedUserId, command.ContractId);

            //if (isCurrentUserContractRenter == false)
            //    AddNotification("AuthenticatedUserId", "Current user is not the contract tenant");

            //if (Invalid)
            //    return new CommandResult(false, "Fix erros below", new { Notifications });

            // Validates contract data
            if (_contractPaymentRepository.CheckIfContractExists(command.ContractId) == false)
                AddNotification("Contract", "This contract does not exist");

            else if (_contractPaymentRepository.CheckIfDateIsAlreadyRegistered(command.ContractId, command.Month) == false)
                AddNotification("Contract", "This date is not registered in a payment cycle for this contract");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var contractPaymentEntityFromDb = _contractPaymentRepository.GetContractPaymentByContractIdAndMonth(command.ContractId, command.Month);
            contractPaymentEntityFromDb.PayRent();

            AddNotifications(contractPaymentEntityFromDb.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractPaymentRepository.ExecutePayment(contractPaymentEntityFromDb);

            var contractPaymentCommandResult = new CommandResult(true, "Payment executed successfuly", new
            {
                contractPaymentEntityFromDb.ContractId,
                contractPaymentEntityFromDb.Month
            });

            return contractPaymentCommandResult;
        }

        public ICommandResult Handle(RejectPaymentCommand command)
        {
            // Validates if the logged user is the renter of the contract
            //var contractDataAuthorization = new ContractDataAuthorization(_contractAuthRepository);
            //bool isCurrentUserContractRenter = contractDataAuthorization.IsCurrentUserContractRenter(authenticatedUserId, command.ContractId);

            //if (isCurrentUserContractRenter == false)
            //    AddNotification("AuthenticatedUserId", "Current user is not the contract renter");

            //if (Invalid)
            //    return new CommandResult(false, "Fix erros below", new { Notifications });

            // Validates contract data
            if (_contractPaymentRepository.CheckIfContractExists(command.ContractId) == false)
                AddNotification("Contract", "This contract does not exist");

            else if (_contractPaymentRepository.CheckIfDateIsAlreadyRegistered(command.ContractId, command.Month) == false)
                AddNotification("Contract", "This date is not registered in a payment cycle for this contract");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var contractPaymentEntityFromDb = _contractPaymentRepository.GetContractPaymentByContractIdAndMonth(command.ContractId, command.Month);
            contractPaymentEntityFromDb.RejectPayment();

            AddNotifications(contractPaymentEntityFromDb.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _contractPaymentRepository.RejectPayment(contractPaymentEntityFromDb);

            var contractPaymentCommandResult = new CommandResult(true, "Payment rejected successfuly", new
            {
                contractPaymentEntityFromDb.ContractId,
                contractPaymentEntityFromDb.Month
            });

            return contractPaymentCommandResult;
        }
    }
}
