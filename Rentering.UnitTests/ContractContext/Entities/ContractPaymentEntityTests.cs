using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.UnitTests.ContractContext.Entities
{
    [TestClass]
    public class ContractPaymentEntityTests
    {
        [TestMethod]
        public void ShouldNotPayRent_WhenRentIsAlreadyPaid()
        {
            var contractId = 1;
            var date = DateTime.Now;
            var contractPayment = new ContractPaymentEntity(contractId, date);

            contractPayment.PayRent();
            contractPayment.PayRent();

            Assert.AreEqual(true, contractPayment.Invalid);
        }

        [TestMethod]
        public void ShouldPayRent_WhenRentIsNotPaidYet()
        {
            var contractId = 1;
            var date = DateTime.Now;
            var contractPayment = new ContractPaymentEntity(contractId, date);

            contractPayment.PayRent();

            Assert.AreEqual(true, contractPayment.Valid);
        }

        [TestMethod]
        public void ShouldNotAcceptPayment_WhenPaymentIsAlreadyAccepted()
        {
            var contractId = 1;
            var date = DateTime.Now;
            var contractPayment = new ContractPaymentEntity(contractId, date);

            contractPayment.AcceptPayment();
            contractPayment.AcceptPayment();

            Assert.AreEqual(true, contractPayment.Invalid);
        }

        [TestMethod]
        public void ShouldAcceptPayment_WhenPaymentIsNotAcceptedYet()
        {
            var contractId = 1;
            var date = DateTime.Now;
            var contractPayment = new ContractPaymentEntity(contractId, date, RenterPaymentStatus.NONE, TentantPaymentStatus.EXECUTED);

            contractPayment.AcceptPayment();

            Assert.AreEqual(true, contractPayment.Valid);
        }

        [TestMethod]
        public void ShouldNotRejectPayment_WhenPaymentIsAlreadyRejected()
        {
            var contractId = 1;
            var date = DateTime.Now;
            var contractPayment = new ContractPaymentEntity(contractId, date);

            contractPayment.RejectPayment();
            contractPayment.RejectPayment();

            Assert.AreEqual(true, contractPayment.Invalid);
        }

        [TestMethod]
        public void ShouldRejectPayment_WhenPaymentIsNotRejectYet()
        {
            var contractId = 1;
            var date = DateTime.Now;
            var contractPayment = new ContractPaymentEntity(contractId, date, RenterPaymentStatus.NONE, TentantPaymentStatus.EXECUTED);

            contractPayment.RejectPayment();

            Assert.AreEqual(true, contractPayment.Valid);
        }
    }
}
