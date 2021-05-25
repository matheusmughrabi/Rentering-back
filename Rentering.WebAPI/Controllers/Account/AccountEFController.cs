using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.ApplicationEF.Commands.Accounts;
using Rentering.Accounts.ApplicationEF.Handlers;
using Rentering.Accounts.InfraEF;
using Rentering.Common.Shared.Commands;
using Rentering.WebAPI.Authorization.Services;
using System.Linq;

namespace Rentering.WebAPI.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountEFController : RenteringBaseController
    {
        private readonly AccountsDbContext _accountsDbContext;

        public AccountEFController(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;
        }

        [HttpGet]
        [Route("v1/Accounts/GetAllAccounts")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllAccounts()
        {
            var result = _accountsDbContext.Account.AsNoTracking().ToList();

            _accountsDbContext.Dispose();

            return Ok(result);
        }

        [HttpGet]
        [Route("v1/Accounts/GetCurrentUser")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int currentUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var result = _accountsDbContext.Account
                .AsNoTracking()
                .Where(c => c.Id == currentUserId)
                .FirstOrDefault();

            _accountsDbContext.Dispose();

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Accounts/Create")]
        public IActionResult CreateAccount([FromBody] CreateAccountCommandEF accountCommand)
        {
            if (User.Identity.IsAuthenticated)
                return Unauthorized("Logout before creating new account");

            var handler = new AccountHandlers(_accountsDbContext);
            var result = handler.Handle(accountCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Accounts/Login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] LoginAccountCommandEF loginCommand)
        {
            var accountEntity = _accountsDbContext.Account
                .AsNoTracking()
                .Where(c => c.Username.Username == loginCommand.Username)
                .FirstOrDefault();

            _accountsDbContext.Dispose();

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
            var handler = new AccountHandlers(_accountsDbContext);
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

            var accountEntity = _accountsDbContext.Account.Where(c => c.Id == accountId).FirstOrDefault();
            _accountsDbContext.Remove(accountEntity);
            _accountsDbContext.Dispose();

            var deletedAccount = new CommandResult(true, "Account deleted successfuly",
                new { UserId = accountId });

            SignOut();

            return Ok(deletedAccount);
        }
    }
}
