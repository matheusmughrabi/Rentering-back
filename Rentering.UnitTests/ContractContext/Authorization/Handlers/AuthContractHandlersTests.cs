using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.Authorization.CommandHandlers;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.QueryResults;
using Rentering.Contracts.Domain.Services;

namespace Rentering.UnitTests.ContractContext.Authorization.Handlers
{
    [TestClass]
    public class AuthContractHandlersTests
    {
        [TestMethod]
        public void ShouldNotValidateCurrentUser_WhenCurrentUserIsNotContractRenter()
        {
            var authContractRenterCommand = new AuthContractRenterCommand(1, 1);
            var retrievedUser = new AuthContractUserProfilesQueryResult(1, 2);
            var retrivedContractParticipants = new AuthContracParticipantsQueryResult(1, 2, 3);

            Mock<IContractAuthRepository> mockAuth = new Mock<IContractAuthRepository>();
            mockAuth.Setup(m => m.GetContractUserProfileIdOfTheCurrentUser(1)).Returns(retrievedUser);
            mockAuth.Setup(m => m.GetContractParticipants(1)).Returns(retrivedContractParticipants);

            Mock<IAuthContractService> mockService = new Mock<IAuthContractService>();
            mockService.Setup(m => m.IsCurrentUserContractRenter(1, 1)).Returns(false);

            var authContractHandler = new AuthContractHandlers(mockService.Object);
            var result = authContractHandler.Handle(authContractRenterCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldValidateCurrentUser_WhenCurrentUserIsContractRenter()
        {
            var authContractRenterCommand = new AuthContractRenterCommand(1, 1);
            var retrievedUser = new AuthContractUserProfilesQueryResult(1, 1);
            var retrivedContractParticipants = new AuthContracParticipantsQueryResult(1, 1, 3);

            Mock<IAuthContractService> mockService = new Mock<IAuthContractService>();
            mockService.Setup(m => m.IsCurrentUserContractRenter(1, 1)).Returns(true);

            var authContractHandler = new AuthContractHandlers(mockService.Object);
            var result = authContractHandler.Handle(authContractRenterCommand);

            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void ShouldNotValidateCurrentUser_WhenCurrentUserIsNotContractTenant()
        {
            var authContractTenantCommand = new AuthContractTenantCommand(1, 1);
            var retrievedUser = new AuthContractUserProfilesQueryResult(1, 1);
            var retrivedContractParticipants = new AuthContracParticipantsQueryResult(1, 1, 3);

            Mock<IContractAuthRepository> mockAuth = new Mock<IContractAuthRepository>();
            mockAuth.Setup(m => m.GetContractUserProfileIdOfTheCurrentUser(1)).Returns(retrievedUser);
            mockAuth.Setup(m => m.GetContractParticipants(1)).Returns(retrivedContractParticipants);

            Mock<IAuthContractService> mockService = new Mock<IAuthContractService>();
            mockService.Setup(m => m.IsCurrentUserContractRenter(1, 1)).Returns(false);

            var authContractHandler = new AuthContractHandlers(mockService.Object);
            var result = authContractHandler.Handle(authContractTenantCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldValidateCurrentUser_WhenCurrentUserIsContractTenant()
        {
            var authContractTenantCommand = new AuthContractTenantCommand(1, 1);
            var retrievedUser = new AuthContractUserProfilesQueryResult(1, 1);
            var retrivedContractParticipants = new AuthContracParticipantsQueryResult(1, 3, 1);

            Mock<IContractAuthRepository> mockAuth = new Mock<IContractAuthRepository>();
            mockAuth.Setup(m => m.GetContractUserProfileIdOfTheCurrentUser(1)).Returns(retrievedUser);
            mockAuth.Setup(m => m.GetContractParticipants(1)).Returns(retrivedContractParticipants);

            Mock<IAuthContractService> mockService = new Mock<IAuthContractService>();
            mockService.Setup(m => m.IsCurrentUserContractTenant(1, 1)).Returns(true);

            var authContractHandler = new AuthContractHandlers(mockService.Object);
            var result = authContractHandler.Handle(authContractTenantCommand);

            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void ShouldNotValidateContractCreation_WhenCurrentUserHasReachedLimitOfContracts()
        {
            var authRenterCommand = new AuthRenterCommand(1);

            Mock<IAuthContractService> mockContractService = new Mock<IAuthContractService>();
            mockContractService.Setup(m => m.HasUserReachedLimitOfContracts(1)).Returns(true);

            var authContractHandler = new AuthContractHandlers(mockContractService.Object);
            var result = authContractHandler.Handle(authRenterCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldValidateContractCreation_WhenCurrentUserHasNotReachedLimitOfContracts()
        {
            var authRenterCommand = new AuthRenterCommand(1);

            Mock<IAuthContractService> mockContractService = new Mock<IAuthContractService>();
            mockContractService.Setup(m => m.HasUserReachedLimitOfContracts(1)).Returns(false);

            var authContractHandler = new AuthContractHandlers(mockContractService.Object);
            var result = authContractHandler.Handle(authRenterCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
