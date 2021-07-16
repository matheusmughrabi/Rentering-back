using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Accounts.Application.Commands.Accounts;
using Rentering.Accounts.Application.Handlers;
using Rentering.Accounts.Domain.Data;
using Rentering.Common.Shared.Commands;
using Rentering.WebAPI.Authorization.Services;
using System.Linq;

namespace Rentering.WebAPI.Controllers.Account
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : RenteringBaseController
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;

        public AccountController(IAccountUnitOfWork accountUnitOfWork)
        {
            _accountUnitOfWork = accountUnitOfWork;
        }

        [HttpGet]
        [Route("v1/Accounts/GetAllAccounts")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetAllAccounts()
        {
            var accountQueryResults = _accountUnitOfWork.AccountQueryRepository.GetAllAccounts_AdminUsageOnly();

            return Ok(accountQueryResults);
        }

        [HttpGet]
        [Route("v1/Accounts/GetCurrentUser")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int currentUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var accountQueryResult = _accountUnitOfWork.AccountQueryRepository.GetAccountById(currentUserId);

            return Ok(accountQueryResult);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateAccount([FromBody] CreateAccountCommand accountCommand)
        {
            if (User.Identity.IsAuthenticated)
                return Unauthorized("Logout before creating new account");

            var handler = new AccountHandlers(_accountUnitOfWork);
            var result = handler.Handle(accountCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginAccountCommand loginCommand)
        {
            var accountEntity = _accountUnitOfWork.AccountCUDRepository.GetAccountForLogin(loginCommand.Username);

            if (accountEntity == null || accountEntity.Password.Password != loginCommand.Password)
                return NotFound(new { Message = "Invalid username or password" });

            var userInfo = TokenService.GenerateToken(accountEntity);

            var response = new CommandResult(true, "Token generated", userInfo);

            return Ok(response);
        }

        [HttpPatch]
        [Route("v1/Accounts/AssignAdminRole")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AssignAdminRole([FromBody] AssignAdminRoleAccountCommand assignAdminRoleCommand)
        {
            var handler = new AccountHandlers(_accountUnitOfWork);
            var result = handler.Handle(assignAdminRoleCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/Accounts/Delete")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Delete()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var accountEntity = _accountUnitOfWork.AccountCUDRepository.Delete(accountId);

            var deletedAccount = new CommandResult(true, "Account deleted successfuly",
                new { UserId = accountId });

            SignOut();

            return Ok(deletedAccount);
        }
    }
}
