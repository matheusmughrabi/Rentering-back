using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.UnitTests.ContractContext.Entities
{
    [TestClass]
    public class EstateContractEntityTests
    {
        string contractName;
        AddressValueObject propertyAddress;
        PropertyRegistrationNumberValueObject propertyRegistrationNumber;
        PriceValueObject rentPrice;
        DateTime rentDueDate;
        DateTime contractStartDate;
        DateTime contractEndDate;

        public EstateContractEntityTests()
        {
            contractName = "Contract 1";
            propertyAddress = new AddressValueObject("Street 1", "Neighborhood 1", "City 1", "12345678", Contracts.Domain.Enums.e_BrazilStates.AC);
            propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(12345);
            rentPrice = new PriceValueObject(1500);
            rentDueDate = DateTime.Now;
            contractStartDate = DateTime.Now;
            contractEndDate = DateTime.Now.AddYears(1).AddDays(1);
        }

        [TestMethod]
        public void ShouldNotInviteParticipant_WhenContractIdIsZero()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contract.InviteParticipant(1, e_ParticipantRole.Renter);

            Assert.AreEqual(false, contract.Valid);
            Assert.AreEqual(0, contract.Participants.Count());
        }

        [TestMethod]
        public void ShouldNotInviteParticipant_WhenParticipantIsAlreadyInThisRole()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            var participant = new AccountContractsEntity(1, 1, e_ParticipantRole.Renter);
            var participants = new List<AccountContractsEntity>();
            participants.Add(participant);

            //contract.IncludeParticipants(participants);

            contract.InviteParticipant(1, e_ParticipantRole.Renter);

            Assert.AreEqual(1, contract.Participants.Count());
        }

        [TestMethod]
        public void ShouldInviteParticipant_WithAcceptedStatus_WhenParticipantIsTheFirstInTheContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.InviteParticipant(1, e_ParticipantRole.Owner);

            Assert.AreEqual(e_ParticipantStatus.Accepted, contract.Participants.Last().Status);
        }

        [TestMethod]
        public void ShouldInviteParticipant_WithInvitedStatus_WhenParticipantIsNotTheFirstInTheContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.InviteParticipant(1, e_ParticipantRole.Owner);
            contract.InviteParticipant(2, e_ParticipantRole.Owner);

            Assert.AreEqual(e_ParticipantStatus.Invited, contract.Participants.Last().Status);
        }

        [TestMethod]
        public void ShouldNotUpdateRentPrice_WhenNegativeRentPriceIsPassed()
        {
            rentPrice = new PriceValueObject(-1500);

            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldUpdateRentPrice_WhenPositiveRentPriceIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(true, contract.Valid);
        }

        [TestMethod]
        public void ShouldNotCreatePaymentCycle_ContractIdIsSupplied()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate.AddYears(2), contractEndDate);

            contract.CreatePaymentCycle();

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldNotCreatePaymentCycle_WhenNegativeMonthSpanIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate.AddYears(2), contractEndDate, 1);

            contract.CreatePaymentCycle();

            Assert.AreEqual(false, contract.Valid);
            Assert.AreEqual(0, contract.Payments.Count);
        }

        [TestMethod]
        public void ShouldCreatePaymentCycle_WhenContractIdIsPassedAndMonthSpanEqualOrGreaterThanOneIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();

            Assert.AreEqual(true, contract.Valid);
            Assert.AreEqual(12, contract.Payments.Count);
        }

        [TestMethod]
        public void ShouldNotExecutePayment_WhenPaymentThatIsNotRegisteredIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();
            contract.ExecutePayment(DateTime.Now.AddYears(2));

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldExecutePayment_WhenRegisteredPaymentThatHasNotBeenExecutedYetIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();
            contract.ExecutePayment(DateTime.Now);

            var payment = contract.Payments.Where(p => p.Month.Year == DateTime.Now.Year && p.Month.Month == DateTime.Now.Month).FirstOrDefault();

            Assert.AreEqual(e_TenantPaymentStatus.EXECUTED, payment.TenantPaymentStatus);
        }

        [TestMethod]
        public void ShouldNotAcceptPayment_WhenPaymentThatIsNotRegisteredIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();
            contract.AcceptPayment(DateTime.Now.AddYears(2));

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldAcceptPayment_WhenRegisteredPaymentThatHasNotBeenExecutedYetIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();
            contract.AcceptPayment(DateTime.Now);

            var payment = contract.Payments.Where(p => p.Month.Year == DateTime.Now.Year && p.Month.Month == DateTime.Now.Month).FirstOrDefault();

            Assert.AreEqual(e_RenterPaymentStatus.ACCEPTED, payment.RenterPaymentStatus);
        }

        [TestMethod]
        public void ShouldNotRejectPayment_WhenPaymentThatIsNotRegisteredIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();
            contract.RejectPayment(DateTime.Now.AddYears(2));

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldRejectPayment_WhenRegisteredPaymentThatHasNotBeenExecutedYetIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();
            contract.RejectPayment(DateTime.Now);

            var payment = contract.Payments.Where(p => p.Month.Year == DateTime.Now.Year && p.Month.Month == DateTime.Now.Month).FirstOrDefault();

            Assert.AreEqual(e_RenterPaymentStatus.REJECTED, payment.RenterPaymentStatus);
        }

        [TestMethod]
        public void ShouldReturnOwedAmount_WithAddedFeesIncluded()
        {
            rentDueDate = DateTime.Now.AddDays(-1);

            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate, 1);

            contract.CreatePaymentCycle();
            var owedAmount = contract.CurrentOwedAmount();

            Assert.AreEqual(1650M, owedAmount);
        }
    }
}
