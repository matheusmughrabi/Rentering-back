using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Application.Authorization.Handlers;
using Rentering.Contracts.Domain.Services;

namespace Rentering.UnitTests.Contract.Authorization.Handlers
{
    [TestClass]
    public class AuthTenantHandlersTests
    {
        [TestMethod]
        public void ShouldNotAuthorizeCurrentUser_WhenCurrentUserIsNotTenantProfileOwner()
        {
            var authCurrentUserAndProfileTenantMatchCommand = new AuthCurrentUserAndProfileTenantMatchCommand(1, 2);

            Mock<IAuthTenantService> mockAuth = new Mock<IAuthTenantService>();
            mockAuth.Setup(m => m.IsCurrentUserTenantProfileOwner(
                authCurrentUserAndProfileTenantMatchCommand.AccountId, authCurrentUserAndProfileTenantMatchCommand.TenantId))
                .Returns(false);

            var authRenterHandler = new AuthTenantHandlers(mockAuth.Object);
            var result = authRenterHandler.Handle(authCurrentUserAndProfileTenantMatchCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldAuthorizeCurrentUser_WhenCurrentUserIsTenantProfileOwner()
        {
            var authCurrentUserAndProfileTenantMatchCommand = new AuthCurrentUserAndProfileTenantMatchCommand(1, 2);

            Mock<IAuthTenantService> mockAuth = new Mock<IAuthTenantService>();
            mockAuth.Setup(m => m.IsCurrentUserTenantProfileOwner(
                authCurrentUserAndProfileTenantMatchCommand.AccountId, authCurrentUserAndProfileTenantMatchCommand.TenantId))
                .Returns(true);

            var authRenterHandler = new AuthTenantHandlers(mockAuth.Object);
            var result = authRenterHandler.Handle(authCurrentUserAndProfileTenantMatchCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
