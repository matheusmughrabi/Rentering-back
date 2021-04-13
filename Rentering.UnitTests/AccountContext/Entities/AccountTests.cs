﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.UnitTests.AccountContext.Entities
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void ShouldNotChangeEmail_WhenInvalidEmailIsPassed()
        {
            var email = new EmailValueObject("matheus@gmail.com");
            var username = new UsernameValueObject("matheus");
            var password = new PasswordValueObject("123456", "123456");

            var renter = new AccountEntity(email, username, password);

            var invalidEmail = new EmailValueObject("meunovoemail");
            renter.ChangeEmail(invalidEmail);

            Assert.AreEqual(true, renter.Invalid);
        }

        [TestMethod]
        public void ShouldNotChangeEmail_WhenSameValidEmailIsPassed()
        {
            var email = new EmailValueObject("matheus@gmail.com");
            var username = new UsernameValueObject("matheus");
            var password = new PasswordValueObject("123456", "123456");

            var renter = new AccountEntity(email, username, password);

            var validEmail = new EmailValueObject("matheus@gmail.com");
            renter.ChangeEmail(validEmail);

            Assert.AreEqual(true, renter.Invalid);
        }

        [TestMethod]
        public void ShouldChangeEmail_WhenDifferentValidEmailIsPassed()
        {
            var email = new EmailValueObject("matheus@gmail.com");
            var username = new UsernameValueObject("matheus");
            var password = new PasswordValueObject("123456", "123456");

            var renter = new AccountEntity(email, username, password);

            var validEmail = new EmailValueObject("matheusnovo@gmail.com");
            renter.ChangeEmail(validEmail);

            Assert.AreEqual(true, renter.Valid);
        }

        [TestMethod]
        public void ShouldNotChangeUsername_WhenInvalidUsernameIsPassed()
        {
            var email = new EmailValueObject("matheus@gmail.com");
            var username = new UsernameValueObject("matheus");
            var password = new PasswordValueObject("123456", "123456");

            var renter = new AccountEntity(email, username, password);

            var invalidUsername = new UsernameValueObject("ma");
            renter.ChangeUsername(invalidUsername);

            Assert.AreEqual(true, renter.Invalid);
        }

        [TestMethod]
        public void ShouldChangeUsername_WhenDifferentValidUsernameIsPassed()
        {
            var email = new EmailValueObject("matheus@gmail.com");
            var username = new UsernameValueObject("matheus");
            var password = new PasswordValueObject("123456", "123456");

            var renter = new AccountEntity(email, username, password);

            var validUsername = new UsernameValueObject("matheusnovo");
            renter.ChangeUsername(validUsername);

            Assert.AreEqual(true, renter.Valid);
        }

        [TestMethod]
        public void ShouldChangeUsername_WhenSameValidUsernameIsPassed()
        {
            var email = new EmailValueObject("matheus@gmail.com");
            var username = new UsernameValueObject("matheus");
            var password = new PasswordValueObject("123456", "123456");

            var renter = new AccountEntity(email, username, password);

            var validUsername = new UsernameValueObject("matheus");
            renter.ChangeUsername(validUsername);

            Assert.AreEqual(true, renter.Invalid);
        }

        [TestMethod]
        public void ShouldNotAssignAdminRole_WhenUserIsAlreadyAdmin()
        {
            var email = new EmailValueObject("matheu@gmail.com");
            var username = new UsernameValueObject("matheus");
            var accountEntity = new AccountEntity(email, username, role: e_Roles.Admin);

            accountEntity.AssignAdminRole();

            Assert.AreEqual(true, accountEntity.Invalid);
        }

        [TestMethod]
        public void ShouldAssignAdminRole_WhenUserIsNotAdminYet()
        {
            var email = new EmailValueObject("matheu@gmail.com");
            var username = new UsernameValueObject("matheus");
            var accountEntity = new AccountEntity(email, username, role: e_Roles.RegularUser);

            accountEntity.AssignAdminRole();

            Assert.AreEqual(true, accountEntity.Valid);
        }
    }
}
