//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Rentering.Contracts.Application.CommandHandlers;
//using Rentering.Contracts.Application.Commands;
//using Rentering.Contracts.Domain.Entities;
//using Rentering.Contracts.Domain.Enums;
//using Rentering.Contracts.Domain.Repositories.CUDRepositories;
//using Rentering.Contracts.Domain.Repositories.QueryRepositories;
//using Rentering.Contracts.Domain.ValueObjects;

//namespace Rentering.UnitTests.ContractContext.Handlers
//{
//    [TestClass]
//    public class TenantHandlersTests
//    {
//        private CreateTenantCommand _createTenantCommand;
//        private UpdateTenantCommand _updateTenantCommand;

//        private int _id;
//        private int _accountId;
//        private string _firstName;
//        private string _lastName;
//        private string _nationality;
//        private string _ocupation;
//        private e_MaritalStatus _maritalStatus;
//        private string _identityRG;
//        private string _cpf;
//        private string _street;
//        private string _neighborhood;
//        private string _city;
//        private string _cep;
//        private e_BrazilStates _state;
//        private string _spouseFirstName;
//        private string _spouseLastName;
//        private string _spouseNationality;
//        private string _spouseOcupation;
//        private string _spouseIdentityRG;
//        private string _spouseCPF;

//        private NameValueObject name;
//        private IdentityRGValueObject identityRG;
//        private CPFValueObject cpf;
//        private AddressValueObject address;
//        private NameValueObject spouseName;
//        private IdentityRGValueObject spouseIdentityRG;
//        private CPFValueObject spouseCPF;

//        private TenantEntity _tenantEntity;

//        public TenantHandlersTests()
//        {
//            _id = 1;
//            _accountId = 1;
//            _firstName = "João";
//            _lastName = "Silva";
//            _nationality = "Brasileiro";
//            _ocupation = "Desenvolvedor";
//            _maritalStatus = e_MaritalStatus.Single;
//            _identityRG = "26.384.185-6";
//            _cpf = "729.533.620-61";
//            _street = "Dom Pedro";
//            _neighborhood = "Vila Nova";
//            _city = "São Paulo";
//            _cep = "08032-200";
//            _state = e_BrazilStates.SP;
//            _spouseFirstName = "Maria";
//            _spouseLastName = "Silva";
//            _spouseNationality = "Brasileiro";
//            _spouseOcupation = "Desenvolvedora";
//            _spouseIdentityRG = "34.254.880-3";
//            _spouseCPF = "667.137.180-60";

//            _createTenantCommand = new CreateTenantCommand(_firstName, _lastName, _nationality, _ocupation, _maritalStatus, _identityRG,
//                _cpf, _street, _neighborhood, _city, _cep, _state, _spouseFirstName, _spouseLastName, _spouseNationality, _spouseOcupation,
//                _spouseIdentityRG, _spouseCPF);

//            _updateTenantCommand = new UpdateTenantCommand(_id, _firstName, _lastName, _nationality, _ocupation, _maritalStatus, _identityRG,
//                _cpf, _street, _neighborhood, _city, _cep, _state, _spouseFirstName, _spouseLastName, _spouseNationality, _spouseOcupation,
//                _spouseIdentityRG, _spouseCPF);

//            name = new NameValueObject(_createTenantCommand.FirstName, _createTenantCommand.LastName);
//            identityRG = new IdentityRGValueObject(_createTenantCommand.IdentityRG);
//            cpf = new CPFValueObject(_createTenantCommand.CPF);
//            address = new AddressValueObject(_createTenantCommand.Street, _createTenantCommand.Neighborhood, _createTenantCommand.City, _createTenantCommand.CEP, _createTenantCommand.State);
//            spouseName = new NameValueObject(_createTenantCommand.SpouseFirstName, _createTenantCommand.SpouseLastName);
//            spouseIdentityRG = new IdentityRGValueObject(_createTenantCommand.SpouseIdentityRG);
//            spouseCPF = new CPFValueObject(_createTenantCommand.CPF);

//            _tenantEntity = new TenantEntity(_accountId, name, _nationality, _ocupation, _maritalStatus, identityRG,
//                cpf, address, spouseName, _spouseNationality, _spouseOcupation, spouseIdentityRG, spouseCPF);
//        }

//        [TestMethod]
//        public void ShouldNotCreateTenant_WhenAccountDoesNotExist()
//        {
//            Mock<ITenantCUDRepository> mock = new Mock<ITenantCUDRepository>();
//            mock.Setup(m => m.Create(_tenantEntity));

//            Mock<ITenantQueryRepository> queryMock = new Mock<ITenantQueryRepository>();
//            queryMock.Setup(m => m.CheckIfAccountExists(_createTenantCommand.AccountId)).Returns(false);

//            var createTenantHandler = new TenantHandlers(mock.Object, queryMock.Object);
//            var result = createTenantHandler.Handle(_createTenantCommand);

//            Assert.AreEqual(false, result.Success);
//        }

//        [TestMethod]
//        public void ShouldCreateTenant_WhenAccountExists()
//        {
//            Mock<ITenantCUDRepository> mock = new Mock<ITenantCUDRepository>();
//            mock.Setup(m => m.Create(_tenantEntity));

//            Mock<ITenantQueryRepository> queryMock = new Mock<ITenantQueryRepository>();
//            queryMock.Setup(m => m.CheckIfAccountExists(_createTenantCommand.AccountId)).Returns(true);

//            var createTenantHandler = new TenantHandlers(mock.Object, queryMock.Object);
//            var result = createTenantHandler.Handle(_createTenantCommand);

//            Assert.AreEqual(true, result.Success);
//        }

//        [TestMethod]
//        public void ShouldNotUpdateTenant_WhenAccountDoesNotExist()
//        {
//            Mock<ITenantCUDRepository> mock = new Mock<ITenantCUDRepository>();
//            mock.Setup(m => m.Update(_updateTenantCommand.Id, _tenantEntity));

//            Mock<ITenantQueryRepository> queryMock = new Mock<ITenantQueryRepository>();
//            queryMock.Setup(m => m.CheckIfAccountExists(_createTenantCommand.AccountId)).Returns(false);

//            var createTenantHandler = new TenantHandlers(mock.Object, queryMock.Object);
//            var result = createTenantHandler.Handle(_updateTenantCommand);

//            Assert.AreEqual(false, result.Success);
//        }

//        [TestMethod]
//        public void ShouldUpdateTenant_WhenAccountExists()
//        {
//            Mock<ITenantCUDRepository> mock = new Mock<ITenantCUDRepository>();
//            mock.Setup(m => m.Update(_updateTenantCommand.Id, _tenantEntity));

//            Mock<ITenantQueryRepository> queryMock = new Mock<ITenantQueryRepository>();
//            queryMock.Setup(m => m.CheckIfAccountExists(_createTenantCommand.AccountId)).Returns(true);

//            var createTenantHandler = new TenantHandlers(mock.Object, queryMock.Object);
//            var result = createTenantHandler.Handle(_updateTenantCommand);

//            Assert.AreEqual(true, result.Success);
//        }
//    }
//}
