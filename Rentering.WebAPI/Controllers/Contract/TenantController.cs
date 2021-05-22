using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.Handlers;
using Rentering.Contracts.Domain.Data;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public TenantController(
            IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        [HttpGet]
        [Route("v1/Tenants/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetTenantById(int id)
        {
            var result = _contractUnitOfWork.TenantQuery.GetById(id);

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

            var handler = new TenantHandlers(_contractUnitOfWork);
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

            var handler = new TenantHandlers(_contractUnitOfWork);
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

            var handler = new TenantHandlers(_contractUnitOfWork);
            var result = handler.Handle(deleteTenantCommand);

            return Ok(result);
        }
    }
}
