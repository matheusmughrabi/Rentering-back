using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.UnitTests.ContractContext.Entities
{
    [TestClass]
    public class RenterEntityTests
    {
        [TestMethod]
        public void ShouldNotAcceptToParticipate_WhenRenterStatusIsAlreadyAccepted()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var renter = new RenterEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            renter.AcceptToParticipate();
            renter.AcceptToParticipate();

            Assert.AreEqual(false, renter.Valid);
        }

        [TestMethod]
        public void ShouldAcceptToParticipate_WhenRenterStatusIsNotAcceptedYet()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var renter = new RenterEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            renter.AcceptToParticipate();
            renter.RefuseToParticipate();
            renter.AcceptToParticipate();

            Assert.AreEqual(true, renter.Valid);
        }

        [TestMethod]
        public void ShouldNotRefuseToParticipate_WhenRenterStatusIsAlreadyRefused()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var renter = new RenterEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            renter.RefuseToParticipate();
            renter.RefuseToParticipate();

            Assert.AreEqual(false, renter.Valid);
        }

        [TestMethod]
        public void ShouldRefuseToParticipate_WhenRenterStatusIsNotRefusedYet()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var renter = new RenterEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            renter.RefuseToParticipate();
            renter.AcceptToParticipate();
            renter.RefuseToParticipate();

            Assert.AreEqual(true, renter.Valid);
        }
    }
}
