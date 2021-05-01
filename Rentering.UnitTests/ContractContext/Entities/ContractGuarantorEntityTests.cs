using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Linq;

namespace Rentering.UnitTests.ContractContext.Entities
{
    [TestClass]
    public class ContractGuarantorEntityTests
    {
        string contractName;
        AddressValueObject address;
        PropertyRegistrationNumberValueObject propertyRegistrationNumber;
        PriceValueObject rentPrice;
        DateTime rentDueDate;
        DateTime contractStartDate;
        DateTime contractEndDate;

        public ContractGuarantorEntityTests()
        {
            contractName = "Contract 1";
            address = new AddressValueObject("Street 1", "Neighborhood 1", "City 1", "12345678", Contracts.Domain.Enums.e_BrazilStates.AC);
            propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(12345);
            rentPrice = new PriceValueObject(1500);
            rentDueDate = DateTime.Now;
            contractStartDate = DateTime.Now;
            contractEndDate = DateTime.Now.AddYears(1);
        }

        [TestMethod]
        public void ShouldNotUpdateRentPrice_WhenNegativeRentPriceIsPassed()
        {
            rentPrice = new PriceValueObject(-1500);

            var contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldUpdateRentPrice_WhenPositiveRentPriceIsPassed()
        {
            var contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(true, contract.Valid);
        }

        [TestMethod]
        public void ShouldCreatePaymentCycle()
        {
            var monthSpan = 12;

            var contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contract.CreatePaymentCycle(monthSpan);

            Assert.AreEqual(monthSpan, contract.Payments.Count);
        }

        [TestMethod]
        public void ShouldExecutePayment()
        {
            var monthSpan = 12;

            var contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contract.CreatePaymentCycle(monthSpan);
            contract.ExecutePayment(DateTime.Now);

            var payment = contract.Payments.Where(p => p.Month.ToShortDateString() == DateTime.Now.ToShortDateString()).FirstOrDefault();

            Assert.AreEqual(e_TenantPaymentStatus.EXECUTED, payment.TenantPaymentStatus);
        }

        [TestMethod]
        public void ShouldReturnCorrectOwedAmount()
        {
            var monthSpan = 12;
            rentDueDate = DateTime.Now.AddDays(-1);

            var contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contract.CreatePaymentCycle(monthSpan);
            var owedAmount = contract.CurrentOwedAmount();

            Assert.AreEqual(1650M, owedAmount);
        }
    }
}
