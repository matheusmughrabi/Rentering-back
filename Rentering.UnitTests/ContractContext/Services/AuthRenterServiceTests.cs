using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
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
            List<GetRenterQueryResult> _renterProfilesFromMockDb = new List<GetRenterQueryResult>();
            _renterProfilesFromMockDb.Add(new GetRenterQueryResult() { Id = 25 });
            _renterProfilesFromMockDb.Add(new GetRenterQueryResult() { Id = 65 });

            Mock<IRenterQueryRepository> mock = new Mock<IRenterQueryRepository>();
            mock.Setup(m => m.GetRenterProfilesOfCurrentUser(_accountId)).Returns(_renterProfilesFromMockDb);

            var authRenterService = new AuthRenterService(mock.Object);
            var isCurrentUserTheOwnerOfRenterProfile = authRenterService.IsCurrentUserTheOwnerOfRenterProfile(_accountId, _renterProfileId);

            Assert.AreEqual(false, isCurrentUserTheOwnerOfRenterProfile);
        }

        [TestMethod]
        public void ShouldReturnTrue_WhenCurrentUserOwnsRenterProfile()
        {
            List<GetRenterQueryResult> _renterProfilesFromMockDb = new List<GetRenterQueryResult>();
            _renterProfilesFromMockDb.Add(new GetRenterQueryResult() { Id = 15 });
            _renterProfilesFromMockDb.Add(new GetRenterQueryResult() { Id = 25 });

            Mock<IRenterQueryRepository> mock = new Mock<IRenterQueryRepository>();
            mock.Setup(m => m.GetRenterProfilesOfCurrentUser(_accountId)).Returns(_renterProfilesFromMockDb);

            var authRenterService = new AuthRenterService(mock.Object);

            var isCurrentUserTheOwnerOfRenterProfile = authRenterService.IsCurrentUserTheOwnerOfRenterProfile(_accountId, _renterProfileId);

            Assert.AreEqual(true, isCurrentUserTheOwnerOfRenterProfile);
        }
    }
}
