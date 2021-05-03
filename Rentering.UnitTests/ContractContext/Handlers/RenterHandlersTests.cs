﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class RenterHandlersTests
    {
        private CreateRenterCommand _createRenterCommand;
        private UpdateRenterCommand _updateRenterCommand;

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
        private string _spouseIdentityRG;
        private string _spouseCPF;

        private NameValueObject name;
        private IdentityRGValueObject identityRG;
        private CPFValueObject cpf;
        private AddressValueObject address;
        private NameValueObject spouseName;
        private IdentityRGValueObject spouseIdentityRG;
        private CPFValueObject spouseCPF;

        private RenterEntity _renterEntity;

        public RenterHandlersTests()
        {
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
            _spouseIdentityRG = "34.254.880-3";
            _spouseCPF = "667.137.180-60";

            _createRenterCommand = new CreateRenterCommand(_firstName, _lastName, _nationality, _ocupation, _maritalStatus, _identityRG,
                _cpf, _street, _neighborhood, _city, _cep, _state, _spouseFirstName, _spouseLastName, _spouseNationality, _spouseIdentityRG, _spouseCPF);

            var renterId = 1;
            _updateRenterCommand = new UpdateRenterCommand(renterId, _firstName, _lastName, _nationality, _ocupation, _maritalStatus, _identityRG,
                _cpf, _street, _neighborhood, _city, _cep, _state, _spouseFirstName, _spouseLastName, _spouseNationality, _spouseIdentityRG, _spouseCPF);


            name = new NameValueObject("João", "Silva");
            identityRG = new IdentityRGValueObject("26.384.185-6");
            cpf = new CPFValueObject("729.533.620-61");
            address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", _state);
            spouseName = new NameValueObject("Maria", "Silva");
            spouseIdentityRG = new IdentityRGValueObject("34.254.880-3");
            spouseCPF = new CPFValueObject("667.137.180-60");


            _renterEntity = new RenterEntity(_accountId, name, _nationality, _ocupation, _maritalStatus, identityRG,
                cpf, address, spouseName, _spouseNationality, spouseIdentityRG, spouseCPF);
        }

        [TestMethod]
        public void ShouldNotCreateRenter_WhenAccountDoesNotExist()
        {
            Mock<IRenterCUDRepository> mock = new Mock<IRenterCUDRepository>();
            mock.Setup(m => m.CreateRenter(_renterEntity));

            Mock<IRenterQueryRepository> queryRepositorymock = new Mock<IRenterQueryRepository>();
            queryRepositorymock.Setup(m => m.CheckIfAccountExists(_createRenterCommand.AccountId)).Returns(false);

            var createRenterHandler = new RenterHandlers(mock.Object, queryRepositorymock.Object);
            var result = createRenterHandler.Handle(_createRenterCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateRenter_WhenAccountExists()
        {
            Mock<IRenterCUDRepository> mock = new Mock<IRenterCUDRepository>();
            mock.Setup(m => m.CreateRenter(_renterEntity));

            Mock<IRenterQueryRepository> queryRepositorymock = new Mock<IRenterQueryRepository>();
            queryRepositorymock.Setup(m => m.CheckIfAccountExists(_createRenterCommand.AccountId)).Returns(true);

            var createRenterHandler = new RenterHandlers(mock.Object, queryRepositorymock.Object);
            var result = createRenterHandler.Handle(_createRenterCommand);

            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void ShouldNotUpdateRenter_WhenAccountDoesNotExist()
        {
            Mock<IRenterCUDRepository> mock = new Mock<IRenterCUDRepository>();
            mock.Setup(m => m.UpdateRenter(_updateRenterCommand.Id, _renterEntity));

            Mock<IRenterQueryRepository> queryRepositorymock = new Mock<IRenterQueryRepository>();
            queryRepositorymock.Setup(m => m.CheckIfAccountExists(_createRenterCommand.AccountId)).Returns(false);

            var createRenterHandler = new RenterHandlers(mock.Object, queryRepositorymock.Object);
            var result = createRenterHandler.Handle(_updateRenterCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldUpdateRenter_WhenAccountExists()
        {
            Mock<IRenterCUDRepository> mock = new Mock<IRenterCUDRepository>();
            mock.Setup(m => m.UpdateRenter(_updateRenterCommand.Id, _renterEntity));

            Mock<IRenterQueryRepository> queryRepositorymock = new Mock<IRenterQueryRepository>();
            queryRepositorymock.Setup(m => m.CheckIfAccountExists(_createRenterCommand.AccountId)).Returns(true);

            var createRenterHandler = new RenterHandlers(mock.Object, queryRepositorymock.Object);
            var result = createRenterHandler.Handle(_updateRenterCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
