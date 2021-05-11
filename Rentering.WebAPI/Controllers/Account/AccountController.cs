﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Accounts.Application.Commands;
using Rentering.Accounts.Application.Handlers;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Accounts.Domain.Extensions;
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
        private readonly IAccountUnitOfWork _accountUnitOfWork;

        public AccountController(IAccountUnitOfWork accountUnitOfWork)
        {
            _accountUnitOfWork = accountUnitOfWork;
        }

        [HttpGet]
        [Route("v1/Accounts/GetAllAccounts")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<GetAccountQueryResult> GetAllAccount()
        {
            var result = _accountUnitOfWork.AccountQuery.GetAccounts();

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

            var result = _accountUnitOfWork.AccountQuery.GetAccountById(currentUserId);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Accounts/Create")]
        public IActionResult CreateAccount([FromBody] CreateAccountCommand accountCommand)
        {
            if (User.Identity.IsAuthenticated)
                return Unauthorized("Logout before creating new account");

            var handler = new AccountHandlers(_accountUnitOfWork);
            var result = handler.Handle(accountCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Accounts/Login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] LoginAccountCommand loginCommand)
        {
            var account = _accountUnitOfWork.AccountQuery.GetAccounts()
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

            _accountUnitOfWork.AccountCUD.Delete(accountId);

            var deletedAccount = new CommandResult(true, "Account deleted successfuly",
                new { UserId = accountId });

            SignOut();

            return Ok(deletedAccount);
        }
    }
}


