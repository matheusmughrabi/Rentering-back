using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;

namespace Rentering.UnitTests.ContractContext.Handlers
{
    [TestClass]
    public class ContractUserProfileHandlersTests
    {
        # region Create User
        [TestMethod]
        public void ShouldNotCreateContratUserProfile_WhenAccountDoesNotExist()
        {
            int accountId = 1;
            var createContractUserProfileCommand = new CreateContractUserProfileCommand(accountId);

            Mock<IContractUserProfileCUDRepository> mock = new Mock<IContractUserProfileCUDRepository>();
            mock.Setup(m => m.CheckIfAccountExists(accountId)).Returns(false);

            var createContractUserProfileHandler = new ContractUserProfileHandlers(mock.Object);
            var result = createContractUserProfileHandler.Handle(createContractUserProfileCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateContractUserProfile_WhenAccountExists()
        {
            int accountId = 1;
            var createContractUserProfileCommand = new CreateContractUserProfileCommand(accountId);

            Mock<IContractUserProfileCUDRepository> mock = new Mock<IContractUserProfileCUDRepository>();
            mock.Setup(m => m.CheckIfAccountExists(accountId)).Returns(true);

            var createContractUserProfileHandler = new ContractUserProfileHandlers(mock.Object);
            var result = createContractUserProfileHandler.Handle(createContractUserProfileCommand);

            Assert.AreEqual(true, result.Success);
        }
        #endregion
    }
}
