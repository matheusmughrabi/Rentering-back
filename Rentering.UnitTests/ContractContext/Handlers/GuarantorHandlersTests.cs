using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.UnitTests.ContractContext.Handlers
{
    [TestClass]
    public class GuarantorHandlersTests
    {
        private CreateGuarantorCommand _createGuarantorCommand;
        private UpdateGuarantorCommand _updateGuarantorCommand;

        private int _id;
        private int _accountId;
        private string _firstName;
        private string _lastName;
        private string _nationality;
        private string _ocupation;
        private e_MaritalStatus _maritalStatus;
        private string _identityRG;
        private string _cpf;
        private string _street;
        private string _neighborhood;
        private string _city;
        private string _cep;
        private e_BrazilStates _state;
        private string _spouseFirstName;
        private string _spouseLastName;
        private string _spouseNationality;
        private string _spouseOcupation;
        private string _spouseIdentityRG;
        private string _spouseCPF;

        private NameValueObject name;
        private IdentityRGValueObject identityRG;
        private CPFValueObject cpf;
        private AddressValueObject address;
        private NameValueObject spouseName;
        private IdentityRGValueObject spouseIdentityRG;
        private CPFValueObject spouseCPF;

        private GuarantorEntity _guarantorEntity;

        public GuarantorHandlersTests()
        {
            _id = 1;
            _accountId = 1;
            _firstName = "João";
            _lastName = "Silva";
            _nationality = "Brasileiro";
            _ocupation = "Desenvolvedor";
            _maritalStatus = e_MaritalStatus.Single;
            _identityRG = "26.384.185-6";
            _cpf = "729.533.620-61";
            _street = "Dom Pedro";
            _neighborhood = "Vila Nova";
            _city = "São Paulo";
            _cep = "08032-200";
            _state = e_BrazilStates.SP;
            _spouseFirstName = "Maria";
            _spouseLastName = "Silva";
            _spouseNationality = "Brasileiro";
            _spouseOcupation = "Desenvolvedora";
            _spouseIdentityRG = "34.254.880-3";
            _spouseCPF = "667.137.180-60";

            _createGuarantorCommand = new CreateGuarantorCommand(_firstName, _lastName, _nationality, _ocupation, _maritalStatus, _identityRG,
                _cpf, _street, _neighborhood, _city, _cep, _state, _spouseFirstName, _spouseLastName, _spouseNationality, _spouseOcupation,
                _spouseIdentityRG, _spouseCPF);

            _updateGuarantorCommand = new UpdateGuarantorCommand(_id, _firstName, _lastName, _nationality, _ocupation, _maritalStatus, _identityRG,
                _cpf, _street, _neighborhood, _city, _cep, _state, _spouseFirstName, _spouseLastName, _spouseNationality, _spouseOcupation,
                _spouseIdentityRG, _spouseCPF);

            name = new NameValueObject(_createGuarantorCommand.FirstName, _createGuarantorCommand.LastName);
            identityRG = new IdentityRGValueObject(_createGuarantorCommand.IdentityRG);
            cpf = new CPFValueObject(_createGuarantorCommand.CPF);
            address = new AddressValueObject(_createGuarantorCommand.Street, _createGuarantorCommand.Neighborhood, _createGuarantorCommand.City, _createGuarantorCommand.CEP, _createGuarantorCommand.State);
            spouseName = new NameValueObject(_createGuarantorCommand.SpouseFirstName, _createGuarantorCommand.SpouseLastName);
            spouseIdentityRG = new IdentityRGValueObject(_createGuarantorCommand.SpouseIdentityRG);
            spouseCPF = new CPFValueObject(_createGuarantorCommand.SpouseCPF);

            _guarantorEntity = new GuarantorEntity(_accountId, name, _nationality, _ocupation, _maritalStatus, identityRG,
                cpf, address, spouseName, _spouseNationality, _spouseOcupation, spouseIdentityRG, spouseCPF);
        }

        [TestMethod]
        public void ShouldNotCreateGuarantor_WhenAccountDoesNotExist()
        {
            Mock<IGuarantorCUDRepository> mock = new Mock<IGuarantorCUDRepository>();
            mock.Setup(m => m.CreateGuarantor(_guarantorEntity));

            Mock<IGuarantorQueryRepository> mockQuery = new Mock<IGuarantorQueryRepository>();
            mockQuery.Setup(m => m.CheckIfAccountExists(_createGuarantorCommand.AccountId)).Returns(false);

            var createTenantHandler = new GuarantorHandlers(mock.Object, mockQuery.Object);
            var result = createTenantHandler.Handle(_createGuarantorCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateGuarantor_WhenAccountExists()
        {
            Mock<IGuarantorCUDRepository> mock = new Mock<IGuarantorCUDRepository>();
            mock.Setup(m => m.CreateGuarantor(_guarantorEntity));

            Mock<IGuarantorQueryRepository> mockQuery = new Mock<IGuarantorQueryRepository>();
            mockQuery.Setup(m => m.CheckIfAccountExists(_createGuarantorCommand.AccountId)).Returns(true);

            var createTenantHandler = new GuarantorHandlers(mock.Object, mockQuery.Object);
            var result = createTenantHandler.Handle(_createGuarantorCommand);

            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void ShouldNotUpdateGuarantor_WhenAccountDoesNotExist()
        {
            Mock<IGuarantorCUDRepository> mock = new Mock<IGuarantorCUDRepository>();
            mock.Setup(m => m.UpdateGuarantor(_updateGuarantorCommand.Id, _guarantorEntity));

            Mock<IGuarantorQueryRepository> mockQuery = new Mock<IGuarantorQueryRepository>();
            mockQuery.Setup(m => m.CheckIfAccountExists(_createGuarantorCommand.AccountId)).Returns(false);

            var updateGuarantorHandler = new GuarantorHandlers(mock.Object, mockQuery.Object);
            var result = updateGuarantorHandler.Handle(_updateGuarantorCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldUpdateGuarantor_WhenAccountExists()
        {
            Mock<IGuarantorCUDRepository> mock = new Mock<IGuarantorCUDRepository>();
            mock.Setup(m => m.UpdateGuarantor(_updateGuarantorCommand.Id, _guarantorEntity));

            Mock<IGuarantorQueryRepository> mockQuery = new Mock<IGuarantorQueryRepository>();
            mockQuery.Setup(m => m.CheckIfAccountExists(_createGuarantorCommand.AccountId)).Returns(true);

            var updateGuarantorHandler = new GuarantorHandlers(mock.Object, mockQuery.Object);
            var result = updateGuarantorHandler.Handle(_updateGuarantorCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
