using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using Rentering.Contracts.Infra.Services;
using System.Collections.Generic;

namespace Rentering.UnitTests.ContractContext.Services
{
    [TestClass]
    public class AuthGuarantorServiceTests
    {
        private int _accountId;
        private int _guarantorProfileId;

        public AuthGuarantorServiceTests()
        {
            _accountId = 1;
            _guarantorProfileId = 15;
        }

        [TestMethod]
        public void ShouldReturnFalse_WhenCurrentUserDoesNotOwnGuarantorProfile()
        {
            List<AuthGuarantorProfilesOfTheCurrentUserQueryResults> _guarantorProfilesFromMockDb = new List<AuthGuarantorProfilesOfTheCurrentUserQueryResults>();
            _guarantorProfilesFromMockDb.Add(new AuthGuarantorProfilesOfTheCurrentUserQueryResults(14));
            _guarantorProfilesFromMockDb.Add(new AuthGuarantorProfilesOfTheCurrentUserQueryResults(13));

            Mock<IGuarantorAuthRepository> mock = new Mock<IGuarantorAuthRepository>();
            mock.Setup(m => m.GetGuarantorProfilesOfTheCurrentUser(_accountId)).Returns(_guarantorProfilesFromMockDb);

            var authGuarantorService = new AuthGuarantorService(mock.Object);
            var isCurrentUserTheOwnerOfGuarantorProfile = authGuarantorService.IsCurrentUserGuarantorProfileOwner(_accountId, _guarantorProfileId);

            Assert.AreEqual(false, isCurrentUserTheOwnerOfGuarantorProfile);
        }

        [TestMethod]
        public void ShouldReturnTrue_WhenCurrentUserOwnsGuarantorProfile()
        {
            List<AuthGuarantorProfilesOfTheCurrentUserQueryResults> _guarantorProfilesFromMockDb = new List<AuthGuarantorProfilesOfTheCurrentUserQueryResults>();
            _guarantorProfilesFromMockDb.Add(new AuthGuarantorProfilesOfTheCurrentUserQueryResults(15));
            _guarantorProfilesFromMockDb.Add(new AuthGuarantorProfilesOfTheCurrentUserQueryResults(14));
            _guarantorProfilesFromMockDb.Add(new AuthGuarantorProfilesOfTheCurrentUserQueryResults(13));

            Mock<IGuarantorAuthRepository> mock = new Mock<IGuarantorAuthRepository>();
            mock.Setup(m => m.GetGuarantorProfilesOfTheCurrentUser(_accountId)).Returns(_guarantorProfilesFromMockDb);

            var authGuarantorService = new AuthGuarantorService(mock.Object);
            var isCurrentUserTheOwnerOfGuarantorProfile = authGuarantorService.IsCurrentUserGuarantorProfileOwner(_accountId, _guarantorProfileId);

            Assert.AreEqual(true, isCurrentUserTheOwnerOfGuarantorProfile);
        }
    }
}
