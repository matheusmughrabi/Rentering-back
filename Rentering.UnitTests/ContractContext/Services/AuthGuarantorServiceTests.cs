using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
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
            List<GetGuarantorQueryResult> _guarantorProfilesFromMockDb = new List<GetGuarantorQueryResult>();
            _guarantorProfilesFromMockDb.Add(new GetGuarantorQueryResult() { Id = 16 });
            _guarantorProfilesFromMockDb.Add(new GetGuarantorQueryResult() { Id = 17 });

            Mock<IGuarantorQueryRepository> mock = new Mock<IGuarantorQueryRepository>();
            mock.Setup(m => m.GetGuarantorProfilesOfCurrentUser(_accountId)).Returns(_guarantorProfilesFromMockDb);

            var authGuarantorService = new AuthGuarantorService(mock.Object);
            var isCurrentUserTheOwnerOfGuarantorProfile = authGuarantorService.IsCurrentUserGuarantorProfileOwner(_accountId, _guarantorProfileId);

            Assert.AreEqual(false, isCurrentUserTheOwnerOfGuarantorProfile);
        }

        [TestMethod]
        public void ShouldReturnTrue_WhenCurrentUserOwnsGuarantorProfile()
        {
            List<GetGuarantorQueryResult> _guarantorProfilesFromMockDb = new List<GetGuarantorQueryResult>();
            _guarantorProfilesFromMockDb.Add(new GetGuarantorQueryResult() { Id = 15 });
            _guarantorProfilesFromMockDb.Add(new GetGuarantorQueryResult() { Id = 17 });

            Mock<IGuarantorQueryRepository> mock = new Mock<IGuarantorQueryRepository>();
            mock.Setup(m => m.GetGuarantorProfilesOfCurrentUser(_accountId)).Returns(_guarantorProfilesFromMockDb);

            var authGuarantorService = new AuthGuarantorService(mock.Object);
            var isCurrentUserTheOwnerOfGuarantorProfile = authGuarantorService.IsCurrentUserGuarantorProfileOwner(_accountId, _guarantorProfileId);

            Assert.AreEqual(true, isCurrentUserTheOwnerOfGuarantorProfile);
        }
    }
}
