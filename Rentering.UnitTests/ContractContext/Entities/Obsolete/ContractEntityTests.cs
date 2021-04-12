using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.UnitTests.ContractContext.Entities
{
    [TestClass]
    public class ContractEntityTests
    {
        [TestMethod]
        public void ShouldBeInvalid_WhenRenterIdAndTenantIdAreEqual()
        {
            var contractName = "amoreira";
            var rentPrice = new PriceValueObject(1000M);
            var renterId = 1;
            var tenantId = 1;
            var contract = new ContractEntity(contractName, rentPrice, renterId, tenantId);

            Assert.AreEqual(true, contract.Invalid);
        }

        [TestMethod]
        public void ShouldBeValid_WhenRenterIdAndTenantIdAreDifferent()
        {
            var contractName = "amoreira";
            var rentPrice = new PriceValueObject(1000M);
            var renterId = 1;
            var tenantId = 2;
            var contract = new ContractEntity(contractName, rentPrice, renterId, tenantId);

            Assert.AreEqual(true, contract.Valid);
        }


        [TestMethod]
        public void ShouldNotUpdateRentPrice_WhenNegativeRentPriceIsPassed()
        {
            var contractName = "amoreira";
            var rentPrice = new PriceValueObject(1000M);
            var renterId = 1;
            var tenantId = 2;
            var contract = new ContractEntity(contractName, rentPrice, renterId, tenantId);

            contract.UpdateRentPrice(new PriceValueObject(-1500));
            Assert.AreEqual(true, contract.Invalid);
        }

        [TestMethod]
        public void ShouldUpdateRentPrice_WhenPositiveRentPriceIsPassed()
        {
            var contractName = "amoreira";
            var rentPrice = new PriceValueObject(1000M);
            var renterId = 1;
            var tenantId = 2;
            var contract = new ContractEntity(contractName, rentPrice, renterId, tenantId);

            contract.UpdateRentPrice(new PriceValueObject(1500));
            Assert.AreEqual(true, contract.Valid);
        }
    }
}
