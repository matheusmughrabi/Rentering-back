//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Rentering.Accounts.Application.Commands;
//using Rentering.Accounts.Application.Handlers;
//using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories;
//using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories;
//using Rentering.Accounts.Domain.Entities;
//using Rentering.Accounts.Domain.ValueObjects;

//namespace Rentering.UnitTests.AccountContext.Handlers
//{
//    [TestClass]
//    public class AccountHandlersTests
//    {
//        private string _email = "nugson@gmail.com";
//        private string _username = "nugson";
//        private string _password = "123456789";
//        private string _confirmPassword = "123456789";

//        CreateAccountCommand _createAccountCommand;

//        EmailValueObject _emailVO;
//        UsernameValueObject _usernameVO;
//        PasswordValueObject _passwordVO;
//        AccountEntity _accountEntity;

//        public AccountHandlersTests()
//        {
//            _createAccountCommand = new CreateAccountCommand(_email, _username, _password, _confirmPassword);

//            _emailVO = new EmailValueObject(_createAccountCommand.Email);
//            _usernameVO = new UsernameValueObject(_createAccountCommand.Username);
//            _passwordVO = new PasswordValueObject(_createAccountCommand.Password, _createAccountCommand.ConfirmPassword);
//            _accountEntity = new AccountEntity(_emailVO, _usernameVO, _passwordVO);
//        }

//        [TestMethod]
//        public void ShouldNotCreateAccount_WhenEmailAlreadyExists()
//        {
//            Mock<IAccountCUDRepository> mock = new Mock<IAccountCUDRepository>();        
//            mock.Setup(m => m.Create(_accountEntity));

//            Mock<IAccountQueryRepository> mockQueryRepository = new Mock<IAccountQueryRepository>();
//            mockQueryRepository.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(true);
//            mockQueryRepository.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(false);

//            var accountHandler = new AccountHandlers(mock.Object, mockQueryRepository.Object);
//            var result = accountHandler.Handle(_createAccountCommand);

//            Assert.AreEqual(false, result.Success);
//        }

//        [TestMethod]
//        public void ShouldNotCreateAccount_WhenUsernameAlreadyExists()
//        {
//            Mock<IAccountCUDRepository> mock = new Mock<IAccountCUDRepository>();
//            mock.Setup(m => m.Create(_accountEntity));

//            Mock<IAccountQueryRepository> mockQueryRepository = new Mock<IAccountQueryRepository>();
//            mockQueryRepository.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(true);
//            mockQueryRepository.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(false);

//            var accountHandler = new AccountHandlers(mock.Object, mockQueryRepository.Object);
//            var result = accountHandler.Handle(_createAccountCommand);

//            Assert.AreEqual(false, result.Success);
//        }

//        [TestMethod]
//        public void ShouldNotCreateAccount_WhenUsernameAndEmailAlreadyExist()
//        {
//            Mock<IAccountCUDRepository> mock = new Mock<IAccountCUDRepository>();
//            mock.Setup(m => m.Create(_accountEntity));

//            Mock<IAccountQueryRepository> mockQueryRepository = new Mock<IAccountQueryRepository>();
//            mockQueryRepository.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(true);
//            mockQueryRepository.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(false);

//            var accountHandler = new AccountHandlers(mock.Object, mockQueryRepository.Object);
//            var result = accountHandler.Handle(_createAccountCommand);

//            Assert.AreEqual(false, result.Success);
//        }

//        [TestMethod]
//        public void ShouldCreateAccount_WhenUsernameAndEmailDoNotExistYet()
//        {
//            Mock<IAccountCUDRepository> mock = new Mock<IAccountCUDRepository>();
//            mock.Setup(m => m.Create(_accountEntity));

//            Mock<IAccountQueryRepository> mockQueryRepository = new Mock<IAccountQueryRepository>();
//            mockQueryRepository.Setup(m => m.CheckIfEmailExists(_createAccountCommand.Email)).Returns(false);
//            mockQueryRepository.Setup(m => m.CheckIfUsernameExists(_createAccountCommand.Username)).Returns(false);

//            var accountHandler = new AccountHandlers(mock.Object, mockQueryRepository.Object);
//            var result = accountHandler.Handle(_createAccountCommand);

//            Assert.AreEqual(true, result.Success);
//        }
//    }
//}
