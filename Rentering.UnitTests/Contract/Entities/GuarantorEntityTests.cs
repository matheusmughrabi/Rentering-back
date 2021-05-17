using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.UnitTests.Contract.Entities
{
    [TestClass]
    public class GuarantorEntityTests
    {
        [TestMethod]
        public void ShouldNotAcceptToParticipate_WhenGuarantorStatusIsAlreadyAccepted()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var guarantor = new GuarantorEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            guarantor.AcceptToParticipate();
            guarantor.AcceptToParticipate();

            Assert.AreEqual(false, guarantor.Valid);
        }

        [TestMethod]
        public void ShouldAcceptToParticipate_WhenGuarantorStatusIsNotAcceptedYet()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var guarantor = new GuarantorEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            guarantor.AcceptToParticipate();
            guarantor.RefuseToParticipate();
            guarantor.AcceptToParticipate();

            Assert.AreEqual(true, guarantor.Valid);
        }

        [TestMethod]
        public void ShouldNotRefuseToParticipate_WhenGuarantorStatusIsAlreadyRefused()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var guarantor = new GuarantorEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            guarantor.RefuseToParticipate();
            guarantor.RefuseToParticipate();

            Assert.AreEqual(false, guarantor.Valid);
        }

        [TestMethod]
        public void ShouldRefuseToParticipate_WhenGuarantorStatusIsNotRefusedYet()
        {
            var name = new NameValueObject("Meg", "Magson");
            var identityRG = new IdentityRGValueObject("123456789");
            var identityCPF = new CPFValueObject("43126701884");
            var address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", e_BrazilStates.SP);
            var guarantor = new GuarantorEntity(1, name, "Brasileira", "Dev", e_MaritalStatus.Single, identityRG, identityCPF, address);

            guarantor.RefuseToParticipate();
            guarantor.AcceptToParticipate();
            guarantor.RefuseToParticipate();

            Assert.AreEqual(true, guarantor.Valid);
        }
    }
}
