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
    public class TenantHandlersTests
    {
        private CreateTenantCommand _createTenantCommand;

        private int _accountId;
        private string _firstName;
        private string _lastName;
        private string _nationality;
        private string _ocupation;
        private e_MaritalStatus _maritalStatus;
        private string _identityRG;
        private string _cpf;
        private string _street;
        private string _bairro;
        private string _cidade;
        private string _cep;
        private string _estado;
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

        private TenantEntity _tenantEntity;

        public TenantHandlersTests()
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
            _bairro = "Vila Nova";
            _cidade = "São Paulo";
            _cep = "08032-200";
            _estado = "SP";
            _spouseFirstName = "Maria";
            _spouseLastName = "Silva";
            _spouseNationality = "Brasileiro";
            _spouseOcupation = "Desenvolvedora";
            _spouseIdentityRG = "34.254.880-3";
            _spouseCPF = "667.137.180-60";

            _createTenantCommand = new CreateTenantCommand(_firstName, _lastName, _nationality, _ocupation, _maritalStatus, _identityRG,
                _cpf, _street, _bairro, _cidade, _cep, _estado, _spouseFirstName, _spouseLastName, _spouseNationality, _spouseOcupation,
                _spouseIdentityRG, _spouseCPF);

            name = new NameValueObject("João", "Silva");
            identityRG = new IdentityRGValueObject("26.384.185-6");
            cpf = new CPFValueObject("729.533.620-61");
            address = new AddressValueObject("Dom Pedro", "Vila Nova", "São Paulo", "08032-200", "SP");
            spouseName = new NameValueObject("Maria", "Silva");
            spouseIdentityRG = new IdentityRGValueObject("34.254.880-3");
            spouseCPF = new CPFValueObject("667.137.180-60");

            _tenantEntity = new TenantEntity(_accountId, name, _nationality, _ocupation, _maritalStatus, identityRG,
                cpf, address, spouseName, _spouseNationality, _spouseOcupation, spouseIdentityRG, spouseCPF);
        }

        [TestMethod]
        public void ShouldNotCreateTenant_WhenAccountDoesNotExist()
        {
            Mock<ITenantCUDRepository> mock = new Mock<ITenantCUDRepository>();
            mock.Setup(m => m.CheckIfAccountExists(_createTenantCommand.AccountId)).Returns(false);
            mock.Setup(m => m.CreateTenant(_tenantEntity));

            var createTenantHandler = new TenantCommandHandlers(mock.Object);
            var result = createTenantHandler.Handle(_createTenantCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateTenant_WhenAccountExists()
        {
            Mock<ITenantCUDRepository> mock = new Mock<ITenantCUDRepository>();
            mock.Setup(m => m.CheckIfAccountExists(_createTenantCommand.AccountId)).Returns(true);
            mock.Setup(m => m.CreateTenant(_tenantEntity));

            var createTenantHandler = new TenantCommandHandlers(mock.Object);
            var result = createTenantHandler.Handle(_createTenantCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
