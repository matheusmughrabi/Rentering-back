using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentering.UnitTests.ContractContext.Handlers
{
    [TestClass]
    public class GuarantorHandlersTests
    {
        private CreateGuarantorCommand _createGuarantorCommand;

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

            name = new NameValueObject("João", "Silva");
            identityRG = new IdentityRGValueObject("26.384.185-6");
            cpf = new CPFValueObject("729.533.620-61");
            address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", _state);
            spouseName = new NameValueObject("Maria", "Silva");
            spouseIdentityRG = new IdentityRGValueObject("34.254.880-3");
            spouseCPF = new CPFValueObject("667.137.180-60");

            _guarantorEntity = new GuarantorEntity(_accountId, name, _nationality, _ocupation, _maritalStatus, identityRG,
                cpf, address, spouseName, _spouseNationality, _spouseOcupation, spouseIdentityRG, spouseCPF);
        }

        [TestMethod]
        public void ShouldNotCreateGuarantor_WhenAccountDoesNotExist()
        {
            Mock<IGuarantorCUDRepository> mock = new Mock<IGuarantorCUDRepository>();
            mock.Setup(m => m.CheckIfAccountExists(_createGuarantorCommand.AccountId)).Returns(false);
            mock.Setup(m => m.CreateGuarantor(_guarantorEntity));

            var createTenantHandler = new GuarantorHandlers(mock.Object);
            var result = createTenantHandler.Handle(_createGuarantorCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateGuarantor_WhenAccountExists()
        {
            Mock<IGuarantorCUDRepository> mock = new Mock<IGuarantorCUDRepository>();
            mock.Setup(m => m.CheckIfAccountExists(_createGuarantorCommand.AccountId)).Returns(true);
            mock.Setup(m => m.CreateGuarantor(_guarantorEntity));

            var createTenantHandler = new GuarantorHandlers(mock.Object);
            var result = createTenantHandler.Handle(_createGuarantorCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
