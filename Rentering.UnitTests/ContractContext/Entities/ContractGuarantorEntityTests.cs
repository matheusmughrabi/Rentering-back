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
        AddressValueObject propertyAddress;
        PropertyRegistrationNumberValueObject propertyRegistrationNumber;
        PriceValueObject rentPrice;
        DateTime rentDueDate;
        DateTime contractStartDate;
        DateTime contractEndDate;

        public ContractGuarantorEntityTests()
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
        public void ShouldNotInviteRenter_WhenRenterIsAssociatedWithAnotherContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var renter = new RenterEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);
            renter.AcceptToParticipate();

            contract.InviteRenter(renter);

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldInviteRenter_WhenRenterIsNotAssociatedWithAnotherContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var renter = new RenterEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            contract.InviteRenter(renter);

            Assert.AreEqual(true, contract.Valid);
        }

        [TestMethod]
        public void ShouldNotInviteTenant_WhenTenantIsAssociatedWithAnotherContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var tenant = new TenantEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);
            tenant.AcceptToParticipate();

            contract.InviteTenant(tenant);

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldInviteTenant_WhenTenantIsNotAssociatedWithAnotherContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var tenant = new TenantEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);
            tenant.AcceptToParticipate();

            Assert.AreEqual(true, contract.Valid);
        }

        [TestMethod]
        public void ShouldNotInviteGuarantor_WhenGuarantorIsAssociatedWithAnotherContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var guarantor = new GuarantorEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);
            guarantor.AcceptToParticipate();

            contract.InviteGuarantor(guarantor);

            Assert.AreEqual(false, contract.Valid);
        }

        [TestMethod]
        public void ShouldInviteGuarantor_WhenGuarantorIsAssociatedWithAnotherContract()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var guarantor = new GuarantorEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);
            guarantor.AcceptToParticipate();

            Assert.AreEqual(true, contract.Valid);
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
        public void ShouldNotCreatePaymentCycle_WhenNegativeMonthSpanIsPassed()
        {
            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate.AddYears(2), contractEndDate);

            Assert.AreEqual(0, contract.Payments.Count);
        }

        [TestMethod]
        public void ShouldCreatePaymentCycle_WhenPositiveMonthSpanIsPassed()
        {
            var monthSpan = 12;

            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            Assert.AreEqual(monthSpan, contract.Payments.Count);
        }

        [TestMethod]
        public void ShouldExecutePayment()
        {
            var monthSpan = 12;

            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            contract.CreatePaymentCycle();
            contract.ExecutePayment(DateTime.Now);

            var payment = contract.Payments.Where(p => p.Month.ToShortDateString() == DateTime.Now.ToShortDateString()).FirstOrDefault();

            Assert.AreEqual(e_TenantPaymentStatus.EXECUTED, payment.TenantPaymentStatus);
        }

        [TestMethod]
        public void ShouldReturnOwedAmount_WithAddedFeesIncluded()
        {
            rentDueDate = DateTime.Now.AddDays(-1);

            var contract = new EstateContractEntity(contractName, propertyAddress, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);

            var owedAmount = contract.CurrentOwedAmount();

            Assert.AreEqual(1650M, owedAmount);
        }
    }
}
