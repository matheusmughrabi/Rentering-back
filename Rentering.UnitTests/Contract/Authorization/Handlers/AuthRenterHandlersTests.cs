using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Application.Authorization.Handlers;
using Rentering.Contracts.Domain.Services;

namespace Rentering.UnitTests.Contract.Authorization.Handlers
{
    [TestClass]
    public class AuthRenterHandlersTests
    {
        [TestMethod]
        public void ShouldNotAuthorizeCurrentUser_WhenCurrentUserIsNotRenterProfileOwner()
        {
            var authCurrentUserAndProfileRenterMatchCommand = new AuthCurrentUserAndProfileRenterMatchCommand(1, 2);

            Mock<IAuthRenterService> mockAuth = new Mock<IAuthRenterService>();
            mockAuth.Setup(m => m.IsCurrentUserTheOwnerOfRenterProfile(
                authCurrentUserAndProfileRenterMatchCommand.AccountId, authCurrentUserAndProfileRenterMatchCommand.RenterId))
                .Returns(false);

            var authRenterHandler = new AuthRenterHandlers(mockAuth.Object);
            var result = authRenterHandler.Handle(authCurrentUserAndProfileRenterMatchCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldAuthorizeCurrentUser_WhenCurrentUserIsRenterProfileOwner()
        {
            var authCurrentUserAndProfileRenterMatchCommand = new AuthCurrentUserAndProfileRenterMatchCommand(1, 2);

            Mock<IAuthRenterService> mockAuth = new Mock<IAuthRenterService>();
            mockAuth.Setup(m => m.IsCurrentUserTheOwnerOfRenterProfile(
                authCurrentUserAndProfileRenterMatchCommand.AccountId, authCurrentUserAndProfileRenterMatchCommand.RenterId))
                .Returns(true);

            var authRenterHandler = new AuthRenterHandlers(mockAuth.Object);
            var result = authRenterHandler.Handle(authCurrentUserAndProfileRenterMatchCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
