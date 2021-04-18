using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using Rentering.Contracts.Infra.Services;
using System.Collections.Generic;

namespace Rentering.UnitTests.ContractContext.Services
{
    [TestClass]
    public class AuthTenantServiceTests
    {
        private int _accountId;
        private int _tenantProfileId;

        public AuthTenantServiceTests()
        {
            _accountId = 1;
            _tenantProfileId = 15;
        }

        [TestMethod]
        public void ShouldReturnFalse_WhenCurrentUserDoesNotOwnTenantProfile()
        {
            List<AuthTenantProfilesOfTheCurrentUserQueryResults> _tenantProfilesFromMockDb = new List<AuthTenantProfilesOfTheCurrentUserQueryResults>();
            _tenantProfilesFromMockDb.Add(new AuthTenantProfilesOfTheCurrentUserQueryResults(14));
            _tenantProfilesFromMockDb.Add(new AuthTenantProfilesOfTheCurrentUserQueryResults(13));

            Mock<ITenantAuthRepository> mock = new Mock<ITenantAuthRepository>();
            mock.Setup(m => m.GetTenantProfilesOfTheCurrentUser(_accountId)).Returns(_tenantProfilesFromMockDb);

            var authTenantService = new AuthTenantService(mock.Object);
            var isCurrentUserTheOwnerOfTenantProfile = authTenantService.IsCurrentUserTenantProfileOwner(_accountId, _tenantProfileId);

            Assert.AreEqual(false, isCurrentUserTheOwnerOfTenantProfile);
        }

        [TestMethod]
        public void ShouldReturnTrue_WhenCurrentUserOwnsTenantProfile()
        {
            List<AuthTenantProfilesOfTheCurrentUserQueryResults> _tenantProfilesFromMockDb = new List<AuthTenantProfilesOfTheCurrentUserQueryResults>();
            _tenantProfilesFromMockDb.Add(new AuthTenantProfilesOfTheCurrentUserQueryResults(15));
            _tenantProfilesFromMockDb.Add(new AuthTenantProfilesOfTheCurrentUserQueryResults(14));
            _tenantProfilesFromMockDb.Add(new AuthTenantProfilesOfTheCurrentUserQueryResults(13));

            Mock<ITenantAuthRepository> mock = new Mock<ITenantAuthRepository>();
            mock.Setup(m => m.GetTenantProfilesOfTheCurrentUser(_accountId)).Returns(_tenantProfilesFromMockDb);

            var authTenantService = new AuthTenantService(mock.Object);
            var isCurrentUserTheOwnerOfTenantProfile = authTenantService.IsCurrentUserTenantProfileOwner(_accountId, _tenantProfileId);

            Assert.AreEqual(true, isCurrentUserTheOwnerOfTenantProfile);
        }
    }
}
