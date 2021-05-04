using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Accounts.Application.CommandHandlers;
using Rentering.Accounts.Application.Commands;
using Rentering.Accounts.Application.QueryResults;
using Rentering.Accounts.Domain.Extensions;
using Rentering.Accounts.Domain.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Repositories.QueryRepositories;
using Rentering.Common.Shared.Commands;
using Rentering.WebAPI.Authorization.Services;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.WebAPI.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : RenteringBaseController
    {
        private readonly IAccountCUDRepository _accountCUDRepository;
        private readonly IAccountQueryRepository _accountQueryRepository;
        public AccountController(IAccountCUDRepository accountCUDRepository, IAccountQueryRepository accountQueryRepository)
        {
            _accountCUDRepository = accountCUDRepository;
            _accountQueryRepository = accountQueryRepository;
        }

        [HttpGet]
        [Route("v1/Accounts/GetAllAccounts")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<GetAccountQueryResult> GetAllAccount()
        {
            var result = _accountQueryRepository.GetAccounts();

            return result;
        }

        [HttpGet]
        [Route("v1/Accounts/GetCurrentUser")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int currentUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var result = _accountQueryRepository.GetAccountById(currentUserId);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Accounts/Create")]
        public IActionResult CreateAccount([FromBody] CreateAccountCommand accountCommand)
        {
            if (User.Identity.IsAuthenticated)
                return Unauthorized("Logout before creating new account");

            var handler = new AccountHandlers(_accountCUDRepository, _accountQueryRepository);
            var result = handler.Handle(accountCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Accounts/Login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] LoginAccountCommand loginCommand)
        {
            var account = _accountQueryRepository.GetAccounts()
                .Where(c => c.Username == loginCommand.Username && c.Password == loginCommand.Password)
                .FirstOrDefault();

            if (account == null)
                return NotFound(new { Message = "Invalid username or password" });

            var accountEntity = account.EntityFromQueryResult();

            var userInfo = TokenService.GenerateToken(accountEntity);

            return new { userInfo };
        }

        [HttpPatch]
        [Route("v1/Accounts/AssignAdminRole")]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignAdminRole([FromBody] AssignAccountCommand assignAdminRoleCommand)
        {
            var handler = new AccountHandlers(_accountCUDRepository, _accountQueryRepository);
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

            _accountCUDRepository.DeleteAccount(accountId);

            var deletedAccount = new CommandResult(true, "Account deleted successfuly",
                new { UserId = accountId });

            SignOut();

            return Ok(deletedAccount);
        }
    }
}


