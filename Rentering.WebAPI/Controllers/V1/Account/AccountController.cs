using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentering.Accounts.Application.Commands.Accounts;
using Rentering.Accounts.Application.Handlers;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Entities;
using Rentering.Common.Shared.Commands;
using Rentering.WebAPI.Security.Models;
using Rentering.WebAPI.Security.Services;

namespace Rentering.WebAPI.Controllers.V1.Account
{
    [Route("api/v1/Accounts")]
    [ApiController]
    public class AccountController : RenteringBaseController
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;

        public AccountController(IAccountUnitOfWork accountUnitOfWork)
        {
            _accountUnitOfWork = accountUnitOfWork;
        }

        #region GetCurrentUser
        [HttpGet]
        [Route("GetCurrentUser")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCurrentUser()
        {
            var accountQueryResult = _accountUnitOfWork.AccountQueryRepository.GetAccountById(GetCurrentUserId());

            return Ok(accountQueryResult);
        }
        #endregion

        #region Register
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterCommand command)
        {
            if (User.Identity.IsAuthenticated)
                return Unauthorized("Faça logout antes de criar uma nova conta.");

            if (command.Invalid)
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            var handler = new AccountHandlers(_accountUnitOfWork);
            var result = handler.Handle(command);

            if (result.Success == false)
                return Ok(result);

            var userInfo = PerformLogin(command.Username, command.Password);

            var response = new CommandResult(true, "Usuário criado com sucesso!", null, userInfo);

            return Ok(response);
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginAccountCommand command)
        {
            if (command.Invalid)
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            var userInfo = PerformLogin(command.Username, command.Password);

            if (userInfo == null)
            {
                var result = new CommandResult(false, "Impossível realizar login", null, userInfo);
                result.AddNotification("Usuário ou senha incorretos.", "Não foi possível realizar o login");

                return Ok(result);
            }     

            var response = new CommandResult(true, "Token gerado!", null, userInfo);

            return Ok(response);
        }
        #endregion

        #region ChangeLicense
        [HttpPut]
        [Route("change-license")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult ChangeLicense([FromBody] ChangeLicenseCommand command)
        {
            if (command.Invalid)
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            command.CurrentUserId = GetCurrentUserId();

            var handler = new AccountHandlers(_accountUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Delete()
        {
            _accountUnitOfWork.AccountCUDRepository.Delete(GetCurrentUserId());

            var deletedAccount = new CommandResult(true, "Conta deletada com sucesso!", null, null);

            SignOut();

            return Ok(deletedAccount);
        }
        #endregion

        private UserInfoModel PerformLogin(string username, string password)
        {
            var accountEntity = _accountUnitOfWork.AccountCUDRepository.GetAccountForLogin(username);

            var passwordHasher = new PasswordHasher<AccountEntity>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(accountEntity, accountEntity.Password, password);

            if (accountEntity == null || passwordVerificationResult != PasswordVerificationResult.Success)
                return null;

            var userInfo = new SecurityService().GenerateToken(accountEntity);
            return userInfo;
        }
    }
}
