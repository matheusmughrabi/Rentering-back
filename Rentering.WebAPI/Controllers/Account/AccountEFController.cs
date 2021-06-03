using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Accounts.Application.Commands.Accounts;
using Rentering.Accounts.Application.Handlers;
using Rentering.Accounts.Domain.Data;
using Rentering.Common.Shared.Commands;
using Rentering.WebAPI.Authorization.Services;

namespace Rentering.WebAPI.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountEFController : RenteringBaseController
    {
        private readonly IAccountUnitOfWorkEF _accountUnitOfWorkEF;

        public AccountEFController(IAccountUnitOfWorkEF accountUnitOfWorkEF)
        {
            _accountUnitOfWorkEF = accountUnitOfWorkEF;
        }

        [HttpGet]
        [Route("v1/Accounts/GetAllAccounts")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllAccounts()
        {
            var accountQueryResults = _accountUnitOfWorkEF.AccountQueryRepositoryEF.GetAllAccounts_AdminUsageOnly();

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

            var accountQueryResult = _accountUnitOfWorkEF.AccountQueryRepositoryEF.GetAccountById(currentUserId);

            return Ok(accountQueryResult);
        }

        [HttpPost]
        [Route("v1/Accounts/Create")]
        public IActionResult CreateAccount([FromBody] CreateAccountCommandEF accountCommand)
        {
            if (User.Identity.IsAuthenticated)
                return Unauthorized("Logout before creating new account");

            var handler = new AccountHandlers(_accountUnitOfWorkEF);
            var result = handler.Handle(accountCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Accounts/Login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] LoginAccountCommandEF loginCommand)
        {
            var accountEntity = _accountUnitOfWorkEF.AccountCUDRepositoryEF.GetAccountForLogin(loginCommand.Username);

            if (accountEntity == null || accountEntity.Password.Password != loginCommand.Password)
                return NotFound(new { Message = "Invalid username or password" });

            var userInfo = TokenService.GenerateToken(accountEntity);

            return new { userInfo };
        }

        [HttpPatch]
        [Route("v1/Accounts/AssignAdminRole")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AssignAdminRole([FromBody] AssignAdminRoleAccountCommandEF assignAdminRoleCommand)
        {
            var handler = new AccountHandlers(_accountUnitOfWorkEF);
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

            var accountEntity = _accountUnitOfWorkEF.AccountCUDRepositoryEF.Delete(accountId);

            var deletedAccount = new CommandResult(true, "Account deleted successfuly",
                new { UserId = accountId });

            SignOut();

            return Ok(deletedAccount);
        }
    }
}
