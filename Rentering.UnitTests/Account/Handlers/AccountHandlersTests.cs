using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rentering.Accounts.Application.Commands;
using Rentering.Accounts.Application.Handlers;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.UnitTests.Account.Handlers
{
    [TestClass]
    public class AccountHandlersTests
    {
        private Mock<IAccountUnitOfWork> accountUnitOfWorkMock;
        private Mock<IAccountQueryRepository> accountQueryRepositoryMock;
        private Mock<IAccountCUDRepository> accountCUDRepositoryMock;

        private string _email = "nugson@gmail.com";
        private string _username = "nugson";
        private string _password = "123456789";
        private string _confirmPassword = "123456789";

        CreateAccountCommand _createAccountCommand;

        EmailValueObject _emailVO;
        UsernameValueObject _usernameVO;
        PasswordValueObject _passwordVO;
        AccountEntity _accountEntity;

        public AccountHandlersTests()
        {
            accountUnitOfWorkMock = new Mock<IAccountUnitOfWork>();
            accountQueryRepositoryMock = new Mock<IAccountQueryRepository>();
            accountCUDRepositoryMock = new Mock<IAccountCUDRepository>();

            accountUnitOfWorkMock.Setup(m => m.AccountQuery).Returns(accountQueryRepositoryMock.Object);
            accountUnitOfWorkMock.Setup(m => m.AccountCUD).Returns(accountCUDRepositoryMock.Object);

            _createAccountCommand = new CreateAccountCommand(_email, _username, _password, _confirmPassword);

            _emailVO = new EmailValueObject(_createAccountCommand.Email);
            _usernameVO = new UsernameValueObject(_createAccountCommand.Username);
            _passwordVO = new PasswordValueObject(_createAccountCommand.Password, _createAccountCommand.ConfirmPassword);
            _accountEntity = new AccountEntity(_emailVO, _usernameVO, _passwordVO);
        }

        [TestMethod]
        public void ShouldNotCreateAccount_WhenEmailAlreadyExists()
        {
            accountQueryRepositoryMock.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(true);
            accountQueryRepositoryMock.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(false);

            accountCUDRepositoryMock.Setup(m => m.Create(_accountEntity));

            var accountHandler = new AccountHandlers(accountUnitOfWorkMock.Object);
            var result = accountHandler.Handle(_createAccountCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldNotCreateAccount_WhenUsernameAlreadyExists()
        {
            accountQueryRepositoryMock.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(false);
            accountQueryRepositoryMock.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(true);

            accountCUDRepositoryMock.Setup(m => m.Create(_accountEntity));

            var accountHandler = new AccountHandlers(accountUnitOfWorkMock.Object);
            var result = accountHandler.Handle(_createAccountCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldNotCreateAccount_WhenUsernameAndEmailAlreadyExist()
        {
            accountQueryRepositoryMock.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(true);
            accountQueryRepositoryMock.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(true);

            accountCUDRepositoryMock.Setup(m => m.Create(_accountEntity));

            var accountHandler = new AccountHandlers(accountUnitOfWorkMock.Object);
            var result = accountHandler.Handle(_createAccountCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void ShouldCreateAccount_WhenUsernameAndEmailDoNotExistYet()
        {
            accountQueryRepositoryMock.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(false);
            accountQueryRepositoryMock.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(false);

            accountCUDRepositoryMock.Setup(m => m.Create(_accountEntity));

            var accountHandler = new AccountHandlers(accountUnitOfWorkMock.Object);
            var result = accountHandler.Handle(_createAccountCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
