using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Authorization.CommandHandlers;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;

namespace Rentering.WebAPI.Controllers.ContractContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantCUDRepository _tenantCUDRepository;
        private readonly ITenantQueryRepository _tenantQueryRepository;
        private readonly IAuthTenantService _authTenantService;

        public TenantController(
            ITenantCUDRepository tenantCUDRepository,
            ITenantQueryRepository tenantQueryRepository,
            IAuthTenantService authTenantService)
        {
            _tenantCUDRepository = tenantCUDRepository;
            _tenantQueryRepository = tenantQueryRepository;
            _authTenantService = authTenantService;
        }

        [HttpGet]
        [Route("v1/Tenants/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetTenantById(int id)
        {
            var result = _tenantQueryRepository.GetTenantById(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("v1/Tenants")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetTenantProfilesOfCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var result = _tenantQueryRepository.GetTenantProfilesOfCurrentUser(accountId);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/CreateTenant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateTenant([FromBody] CreateTenantCommand createTenantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            createTenantCommand.AccountId = accountId;

            var handler = new TenantHandlers(_tenantCUDRepository);
            var result = handler.Handle(createTenantCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/UpdateTenant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult UpdateTenant([FromBody] UpdateTenantCommand updateTenantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthCurrentUserAndProfileTenantMatchCommand(accountId, updateTenantCommand.Id);
            var authHandler = new AuthTenantHandlers(_authTenantService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            updateTenantCommand.AccountId = accountId;

            var handler = new TenantHandlers(_tenantCUDRepository);
            var result = handler.Handle(updateTenantCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/DeleteTenant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult DeleteTenant([FromBody] DeleteTenantCommand deleteTenantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthCurrentUserAndProfileTenantMatchCommand(authenticatedUserId, deleteTenantCommand.Id);
            var authHandler = new AuthTenantHandlers(_authTenantService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new TenantHandlers(_tenantCUDRepository);
            var result = handler.Handle(deleteTenantCommand);

            return Ok(result);
        }
    }
}
