using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Accounts.Application.Commands.Accounts;
using Rentering.Accounts.Application.Handlers;
using Rentering.Accounts.Domain.Data;
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
        public IActionResult Register([FromBody] RegisterCommand accountCommand)
        {
            if (User.Identity.IsAuthenticated)
                return Unauthorized("Faça logout antes de criar uma nova conta.");

            var handler = new AccountHandlers(_accountUnitOfWork);
            var result = handler.Handle(accountCommand);

            if (result.Success == false)
                return Ok(result);

            var userInfo = PerformLogin(accountCommand.Username, accountCommand.Password);

            var response = new CommandResult(true, "Token gerado", null, userInfo);

            return Ok(response);
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginAccountCommand loginCommand)
        {
            var userInfo = PerformLogin(loginCommand.Username, loginCommand.Password);

            if (userInfo == null)
                return NotFound("Usuário ou senha incorretos");

            var response = new CommandResult(true, "Token gerado", null, userInfo);

            return Ok(response);
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

            if (accountEntity == null || accountEntity.Password.Password != password)
                return null;

            var userInfo = new SecurityService().GenerateToken(accountEntity);
            return userInfo;
        }
    }
}
