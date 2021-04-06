using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;

namespace Rentering.UnitTests.ContractContext.Handlers
{
    [TestClass]
    public class ContractHandlersTests
    {
        #region CreateContract
        [TestMethod]
        public void ShouldNotCreateContract_WhenContractNameExists()
        {
            var createContractCommand = new CreateContractCommand("Meg Contract", 150, 2);

            Mock<IContractCUDRepository> mock = new Mock<IContractCUDRepository>();
            mock.Setup(m => m.CheckIfContractNameExists(createContractCommand.ContractName)).Returns(true);

            var createContractHandler = new ContractHandlers(mock.Object);
            var result = createContractHandler.Handle(createContractCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateContract_WhenContractNameIsUnique()
        {
            var createContractCommand = new CreateContractCommand("Meg Contract", 150,2);

            Mock<IContractCUDRepository> mock = new Mock<IContractCUDRepository>();
            mock.Setup(m => m.CheckIfContractNameExists(createContractCommand.ContractName)).Returns(false);

            var createContractHandler = new ContractHandlers(mock.Object);
            var result = createContractHandler.Handle(createContractCommand);

            Assert.AreEqual(true, result.Success);
        }
        #endregion
    }
}
