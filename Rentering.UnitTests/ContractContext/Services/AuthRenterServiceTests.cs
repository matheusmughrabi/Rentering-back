using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using Rentering.Contracts.Infra.Services;
using System.Collections.Generic;

namespace Rentering.UnitTests.ContractContext.Services
{
    [TestClass]
    public class AuthRenterServiceTests
    {
        private int _accountId;
        private int _renterProfileId;

        public AuthRenterServiceTests()
        {
            _accountId = 1;
            _renterProfileId = 15;
        }

        [TestMethod]
        public void ShouldReturnFalse_WhenCurrentUserDoesNotOwnRenterProfile()
        {
            List<AuthRenterProfilesOfTheCurrentUserQueryResults> _renterProfilesFromMockDb = new List<AuthRenterProfilesOfTheCurrentUserQueryResults>();
            _renterProfilesFromMockDb.Add(new AuthRenterProfilesOfTheCurrentUserQueryResults(14));
            _renterProfilesFromMockDb.Add(new AuthRenterProfilesOfTheCurrentUserQueryResults(13));

            Mock<IRenterAuthRepository> mock = new Mock<IRenterAuthRepository>();
            mock.Setup(m => m.GetRenterProfilesOfTheCurrentUser(_accountId)).Returns(_renterProfilesFromMockDb);

            var authRenterService = new AuthRenterService(mock.Object);
            var isCurrentUserTheOwnerOfRenterProfile = authRenterService.IsCurrentUserTheOwnerOfRenterProfile(_accountId, _renterProfileId);

            Assert.AreEqual(false, isCurrentUserTheOwnerOfRenterProfile);
        }

        [TestMethod]
        public void ShouldReturnTrue_WhenCurrentUserOwnsRenterProfile()
        {
            List<AuthRenterProfilesOfTheCurrentUserQueryResults> _renterProfilesFromMockDb = new List<AuthRenterProfilesOfTheCurrentUserQueryResults>();
            _renterProfilesFromMockDb.Add(new AuthRenterProfilesOfTheCurrentUserQueryResults(15));
            _renterProfilesFromMockDb.Add(new AuthRenterProfilesOfTheCurrentUserQueryResults(14));
            _renterProfilesFromMockDb.Add(new AuthRenterProfilesOfTheCurrentUserQueryResults(13));

            Mock<IRenterAuthRepository> mock = new Mock<IRenterAuthRepository>();
            mock.Setup(m => m.GetRenterProfilesOfTheCurrentUser(_accountId)).Returns(_renterProfilesFromMockDb);

            var authRenterService = new AuthRenterService(mock.Object);
            var isCurrentUserTheOwnerOfRenterProfile = authRenterService.IsCurrentUserTheOwnerOfRenterProfile(_accountId, _renterProfileId);

            Assert.AreEqual(true, isCurrentUserTheOwnerOfRenterProfile);
        }
    }
}
