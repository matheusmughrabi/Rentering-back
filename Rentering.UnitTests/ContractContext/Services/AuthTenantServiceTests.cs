using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
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
            List<GetTenantQueryResult> _tenantProfilesFromMockDb = new List<GetTenantQueryResult>();
            _tenantProfilesFromMockDb.Add(new GetTenantQueryResult());
            _tenantProfilesFromMockDb.Add(new GetTenantQueryResult());

            Mock<ITenantQueryRepository> mock = new Mock<ITenantQueryRepository>();
            mock.Setup(m => m.GetTenantProfilesOfCurrentUser(_accountId)).Returns(_tenantProfilesFromMockDb);

            var authTenantService = new AuthTenantService(mock.Object);
            var isCurrentUserTheOwnerOfTenantProfile = authTenantService.IsCurrentUserTenantProfileOwner(_accountId, _tenantProfileId);

            Assert.AreEqual(false, isCurrentUserTheOwnerOfTenantProfile);
        }

        [TestMethod]
        public void ShouldReturnTrue_WhenCurrentUserOwnsTenantProfile()
        {
            List<GetTenantQueryResult> _tenantProfilesFromMockDb = new List<GetTenantQueryResult>();
            _tenantProfilesFromMockDb.Add(new GetTenantQueryResult());
            _tenantProfilesFromMockDb.Add(new GetTenantQueryResult());

            Mock<ITenantQueryRepository> mock = new Mock<ITenantQueryRepository>();
            mock.Setup(m => m.GetTenantProfilesOfCurrentUser(_accountId)).Returns(_tenantProfilesFromMockDb);

            var authTenantService = new AuthTenantService(mock.Object);
            var isCurrentUserTheOwnerOfTenantProfile = authTenantService.IsCurrentUserTenantProfileOwner(_accountId, _tenantProfileId);

            Assert.AreEqual(true, isCurrentUserTheOwnerOfTenantProfile);
        }
    }
}
