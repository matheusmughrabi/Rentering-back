using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System;

namespace Rentering.UnitTests.ContractContext.Handlers
{
    [TestClass]
    public class ContractPaymentHandlersTests
    {

        #region Create PaymentCycle
        //[TestMethod]
        //public void ShouldNotCreatePaymentCycle_WhenContractDoesNotExist()
        //{
        //    var createPaymentCycleCommand = new CreatePaymentCycleCommand(5, new DateTime(2001, 01, 01));

        //    var retrievedUser = new AuthContractUserProfilesQueryResult(1, 1);
        //    var retrivedContractParticipants = new AuthContracParticipantsQueryResult(1, 1, 2);

        //    Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
        //    mock.Setup(m => m.CheckIfContractExists(createPaymentCycleCommand.ContractId)).Returns(false);
        //    mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(createPaymentCycleCommand.ContractId, createPaymentCycleCommand.Month)).Returns(false);

        //    Mock<IContractContextAuthRepository> mockAuth = new Mock<IContractContextAuthRepository>();
        //    mockAuth.Setup(m => m.LoggedUserContractUserProfileId(1)).Returns(retrievedUser);
        //    mockAuth.Setup(m => m.ContractParticipants(1)).Returns(retrivedContractParticipants);

        //    var createPaymentCycleHandler = new ContractPaymentHandlers(mock.Object, mockAuth.Object);
        //    var result = createPaymentCycleHandler.Handle(createPaymentCycleCommand, 1);

        //    Assert.AreEqual(false, result.Success);
        //}

        [TestMethod]
        public void ShouldNotCreatePaymentCycle_WhenContractExistsButMonthIsAlreadyRegistered()
        {
            var createPaymentCycleCommand = new CreatePaymentCycleCommand(1, new DateTime(2000, 01, 01));

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(createPaymentCycleCommand.ContractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(createPaymentCycleCommand.ContractId, createPaymentCycleCommand.Month)).Returns(true);

            var createPaymentCycleHandler = new ContractPaymentHandlers(mock.Object);
            var result = createPaymentCycleHandler.Handle(createPaymentCycleCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreatePaymentCycle_WhenContractExistsAndMonthIsNotRegisteredYet()
        {
            var createPaymentCycleCommand = new CreatePaymentCycleCommand(1, new DateTime(2001, 01, 01));

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(createPaymentCycleCommand.ContractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(createPaymentCycleCommand.ContractId, createPaymentCycleCommand.Month)).Returns(false);

            var createPaymentCycleHandler = new ContractPaymentHandlers(mock.Object);
            var result = createPaymentCycleHandler.Handle(createPaymentCycleCommand);

            Assert.AreEqual(true, result.Success);
        }
        #endregion

        #region  Accept Payment
        [TestMethod]
        public void ShouldNotAcceptPayment_WhenSpecifiedMonthIsAlreadyAccepted()
        {
            var contractId = 1;
            var month = new DateTime(2000, 01, 01);
            var acceptPaymentCommand = new AcceptPaymentCommand(contractId, month);
            var contractPaymentFromMockDb = new ContractPaymentEntity(contractId, month, e_RenterPaymentStatus.ACCEPTED, e_TenantPaymentStatus.EXECUTED);

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(acceptPaymentCommand.ContractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(acceptPaymentCommand.ContractId, acceptPaymentCommand.Month)).Returns(true);
            mock.Setup(m => m.GetContractPaymentByContractIdAndMonth(acceptPaymentCommand.ContractId, acceptPaymentCommand.Month))
                .Returns(contractPaymentFromMockDb);

            var acceptPaymentHandler = new ContractPaymentHandlers(mock.Object);
            var result = acceptPaymentHandler.Handle(acceptPaymentCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldAcceptPayment_WhenSpecifiedMonthIsNotAcceptedYet()
        {
            var contractId = 1;
            var month = DateTime.Now;

            var contractPaymentFromMockDb = new ContractPaymentEntity(contractId, month, e_RenterPaymentStatus.NONE, e_TenantPaymentStatus.EXECUTED);

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(contractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(contractId, month)).Returns(true);
            mock.Setup(m => m.GetContractPaymentByContractIdAndMonth(contractId, month)).Returns(contractPaymentFromMockDb);

            var acceptPaymentCommand = new AcceptPaymentCommand(contractId, month);

            var acceptPaymentHandler = new ContractPaymentHandlers(mock.Object);
            var result = acceptPaymentHandler.Handle(acceptPaymentCommand);

            Assert.AreEqual(true, result.Success);
        }
        #endregion

        #region  Reject Payment
        [TestMethod]
        public void ShouldNotRejectPayment_WhenSpecifiedMonthIsAlreadyRejected()
        {
            var contractId = 1;
            var month = DateTime.Now;

            var rejectPaymentCommand = new RejectPaymentCommand(1, new DateTime(2000, 02, 01));
            var contractPaymentFromMockDb = new ContractPaymentEntity(contractId, month, e_RenterPaymentStatus.REJECTED, e_TenantPaymentStatus.EXECUTED);

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(rejectPaymentCommand.ContractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(rejectPaymentCommand.ContractId, rejectPaymentCommand.Month)).Returns(true);
            mock.Setup(m => m.GetContractPaymentByContractIdAndMonth(rejectPaymentCommand.ContractId, rejectPaymentCommand.Month))
                .Returns(contractPaymentFromMockDb);

            var acceptPaymentHandler = new ContractPaymentHandlers(mock.Object);
            var result = acceptPaymentHandler.Handle(rejectPaymentCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldRejectPayment_WhenSpecifiedMonthIsNotRejectedYet()
        {
            var contractId = 1;
            var month = DateTime.Now;

            var contractPaymentFromMockDb = new ContractPaymentEntity(contractId, month, e_RenterPaymentStatus.NONE, e_TenantPaymentStatus.EXECUTED);

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(contractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(contractId, month)).Returns(true);
            mock.Setup(m => m.GetContractPaymentByContractIdAndMonth(contractId, month)).Returns(contractPaymentFromMockDb);

            var rejectPaymentCommand = new RejectPaymentCommand(contractId, month);

            var acceptPaymentHandler = new ContractPaymentHandlers(mock.Object);
            var result = acceptPaymentHandler.Handle(rejectPaymentCommand);

            Assert.AreEqual(true, result.Success);
        }
        #endregion

        #region  Execute Payment
        [TestMethod]
        public void ShouldNotExecutePayment_WhenSpecifiedMonthIsAlreadyExecuted()
        {
            var contractId = 1;
            var month = DateTime.Now;

            var executePaymentCommand = new ExecutePaymentCommand(contractId, month);
            var contractPaymentFromMockDb = new ContractPaymentEntity(contractId, month, e_RenterPaymentStatus.NONE, e_TenantPaymentStatus.EXECUTED);

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(contractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(contractId, month)).Returns(true);
            mock.Setup(m => m.GetContractPaymentByContractIdAndMonth(contractId, month)).Returns(contractPaymentFromMockDb);

            var acceptPaymentHandler = new ContractPaymentHandlers(mock.Object);
            var result = acceptPaymentHandler.Handle(executePaymentCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldExecutePayment_WhenSpecifiedMonthIsNotExecutedYet()
        {
            var contractId = 1;
            var month = DateTime.Now;

            var executePaymentCommand = new ExecutePaymentCommand(contractId, month);
            var contractPaymentFromMockDb = new ContractPaymentEntity(contractId, month, e_RenterPaymentStatus.NONE, e_TenantPaymentStatus.NONE);

            Mock<IContractPaymentCUDRepository> mock = new Mock<IContractPaymentCUDRepository>();
            mock.Setup(m => m.CheckIfContractExists(contractId)).Returns(true);
            mock.Setup(m => m.CheckIfDateIsAlreadyRegistered(contractId, month)).Returns(true);
            mock.Setup(m => m.GetContractPaymentByContractIdAndMonth(contractId, month)).Returns(contractPaymentFromMockDb);

            var acceptPaymentHandler = new ContractPaymentHandlers(mock.Object);
            var result = acceptPaymentHandler.Handle(executePaymentCommand);

            Assert.AreEqual(true, result.Success);
        }
        #endregion
    }
}
