using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;
using System;

namespace Rentering.UnitTests.ContractContext.Entities
{
    [TestClass]
    public class EstateContractGuarantorEntityTests
    {
        string contractName;
        int renterId;
        int renterAccountId;
        int tenantId;
        int tenantAccountId;
        int guarantorId;
        int guarantorAccountId;
        AddressValueObject address;
        PropertyRegistrationNumberValueObject propertyRegistrationNumber;
        PriceValueObject rentPrice;
        DateTime rentDueDate;
        DateTime contractStartDate;
        DateTime contractEndDate;

        public EstateContractGuarantorEntityTests()
        {
            contractName = "Contract 1";
            renterId = 1;
            renterAccountId = 2;
            tenantId = 3;
            tenantAccountId = 4;
            guarantorId = 5;
            guarantorAccountId = 6;
            address = new AddressValueObject("Street 1", "Neighborhood 1", "City 1", "12345678", Contracts.Domain.Enums.e_BrazilStates.AC);
            propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(12345);
            rentPrice = new PriceValueObject(1500);
            rentDueDate = DateTime.Now;
            contractStartDate = DateTime.Now;
            contractEndDate = DateTime.Now.AddYears(1);
        }

        [TestMethod]
        public void ShouldBeInvalid_WhenRenterIdAndTenantIdAreEqual()
        {
            renterId = 1;
            tenantId = 1;

            var contract = new ContractWithGuarantorEntity(contractName, renterId, renterAccountId, tenantId, tenantAccountId, guarantorId, guarantorAccountId, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldBeValid_WhenRenterIdAndTenantIdAreDifferent()
        {
            var contract = new ContractWithGuarantorEntity(contractName, renterId, renterAccountId, tenantId, tenantAccountId, guarantorId, guarantorAccountId, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(true, contract.Valid);
        }


        [TestMethod]
        public void ShouldNotUpdateRentPrice_WhenNegativeRentPriceIsPassed()
        {
            rentPrice = new PriceValueObject(-1500);

            var contract = new ContractWithGuarantorEntity(contractName, renterId, renterAccountId, tenantId, tenantAccountId, guarantorId, guarantorAccountId, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldUpdateRentPrice_WhenPositiveRentPriceIsPassed()
        {
            var contract = new ContractWithGuarantorEntity(contractName, renterId, renterAccountId, tenantId, tenantAccountId, guarantorId, guarantorAccountId, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(true, contract.Valid);
        }
    }
}
