using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.ValueObjects;
using System;

namespace Rentering.UnitTests.ContractContext.Handlers
{
    [TestClass]
    public class ContractWithGuarantorHandlersTests
    {
        private ContractWithGuarantorEntity contract;
        private CreateContractGuarantorCommand createContractCommand;
        private string contractName;

        public ContractWithGuarantorHandlersTests()
        {

            contractName = "contrato 1";
            var address = new AddressValueObject("Rua 1", "Bairro 1", "Cidade 1", "14700900", Contracts.Domain.Enums.e_BrazilStates.SP);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(123456789);
            var rentPrice = new PriceValueObject(1500M);
            var rentDueDate = DateTime.Now;
            var contractStartDate = DateTime.Now;
            var contractEndDate = DateTime.Now.AddYears(1);

            createContractCommand = new CreateContractGuarantorCommand(contractName, address.Street, address.Neighborhood, address.City, address.CEP, address.State, propertyRegistrationNumber.Number, rentPrice.Price, rentDueDate, contractStartDate, contractEndDate);

            contract = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, rentDueDate, contractStartDate, contractEndDate);
        }

        [TestMethod]
        public void ShouldNotCreateContract_WhenContractNameAlreadyExists()
        {
            Mock<IContractWithGuarantorCUDRepository> contractCUDRepositoryMock = new Mock<IContractWithGuarantorCUDRepository>();
            contractCUDRepositoryMock.Setup(m => m.CreateContract(contract));

            Mock<IContractWithGuarantorQueryRepository> contractQueryRepositoryMock = new Mock<IContractWithGuarantorQueryRepository>();
            contractQueryRepositoryMock.Setup(m => m.CheckIfContractNameExists(contractName)).Returns(true);

            var createTenantHandler = new ContractGuarantorHandlers(contractCUDRepositoryMock.Object, contractQueryRepositoryMock.Object);
            var result = createTenantHandler.Handle(createContractCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateContract_WhenContractNameDoesNotExistsYet()
        {
            Mock<IContractWithGuarantorCUDRepository> contractCUDRepositoryMock = new Mock<IContractWithGuarantorCUDRepository>();
            contractCUDRepositoryMock.Setup(m => m.CreateContract(contract));

            Mock<IContractWithGuarantorQueryRepository> contractQueryRepositoryMock = new Mock<IContractWithGuarantorQueryRepository>();
            contractQueryRepositoryMock.Setup(m => m.CheckIfContractNameExists(contractName)).Returns(false);

            var createTenantHandler = new ContractGuarantorHandlers(contractCUDRepositoryMock.Object, contractQueryRepositoryMock.Object);
            var result = createTenantHandler.Handle(createContractCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
