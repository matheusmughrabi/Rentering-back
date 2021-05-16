using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.UnitTests.ContractContext.Entities
{
    [TestClass]
    public class TenantEntityTests
    {
        [TestMethod]
        public void ShouldNotAcceptToParticipate_WhenTenantStatusIsAlreadyAccepted()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var tenant = new TenantEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            tenant.AcceptToParticipate();
            tenant.AcceptToParticipate();

            Assert.AreEqual(false, tenant.Valid);
        }

        [TestMethod]
        public void ShouldAcceptToParticipate_WhenTenantStatusIsNotAcceptedYet()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var tenant = new TenantEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            tenant.AcceptToParticipate();
            tenant.RefuseToParticipate();
            tenant.AcceptToParticipate();

            Assert.AreEqual(true, tenant.Valid);
        }

        [TestMethod]
        public void ShouldNotRefuseToParticipate_WhenTenantStatusIsAlreadyRefused()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var tenant = new TenantEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            tenant.RefuseToParticipate();
            tenant.RefuseToParticipate();

            Assert.AreEqual(false, tenant.Valid);
        }

        [TestMethod]
        public void ShouldRefuseToParticipate_WhenTenantStatusIsNotRefusedYet()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var tenant = new TenantEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            tenant.RefuseToParticipate();
            tenant.AcceptToParticipate();
            tenant.RefuseToParticipate();

            Assert.AreEqual(true, tenant.Valid);
        }
    }
}
