using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Application.Authorization.Handlers;
using Rentering.Contracts.Domain.Services;

namespace Rentering.UnitTests.ContractContext.Authorization.Handlers
{
    [TestClass]
    public class AuthGuarantorHandlersTests
    {
        [TestMethod]
        public void ShouldNotAuthorizeCurrentUser_WhenCurrentUserIsNotGuarantorProfileOwner()
        {
            var authCurrentUserAndProfileGuarantorMatchCommand = new AuthCurrentUserAndProfileGuarantorMatchCommand(1, 2);

            Mock<IAuthGuarantorService> mockAuth = new Mock<IAuthGuarantorService>();
            mockAuth.Setup(m => m.IsCurrentUserGuarantorProfileOwner(
                authCurrentUserAndProfileGuarantorMatchCommand.AccountId, authCurrentUserAndProfileGuarantorMatchCommand.GuarantorId))
                .Returns(false);

            var authGuarantorHandler = new AuthGuarantorHandlers(mockAuth.Object);
            var result = authGuarantorHandler.Handle(authCurrentUserAndProfileGuarantorMatchCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldAuthorizeCurrentUser_WhenCurrentUserIsGuarantorProfileOwner()
        {
            var authCurrentUserAndProfileGuarantorMatchCommand = new AuthCurrentUserAndProfileGuarantorMatchCommand(1, 2);

            Mock<IAuthGuarantorService> mockAuth = new Mock<IAuthGuarantorService>();
            mockAuth.Setup(m => m.IsCurrentUserGuarantorProfileOwner(
                authCurrentUserAndProfileGuarantorMatchCommand.AccountId, authCurrentUserAndProfileGuarantorMatchCommand.GuarantorId))
                .Returns(true);

            var authGuarantorHandler = new AuthGuarantorHandlers(mockAuth.Object);
            var result = authGuarantorHandler.Handle(authCurrentUserAndProfileGuarantorMatchCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
