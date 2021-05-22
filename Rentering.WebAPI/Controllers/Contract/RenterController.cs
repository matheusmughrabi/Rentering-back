using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.Handlers;
using Rentering.Contracts.Domain.Data;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenterController : RenteringBaseController
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public RenterController(
            IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        [HttpGet]
        [Route("v1/Renters/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetRenterById(int id)
        {
            var result = _contractUnitOfWork.RenterQuery.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/CreateRenter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateRenter([FromBody] CreateRenterCommand createRenterCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new RenterHandlers(_contractUnitOfWork);
            var result = handler.Handle(createRenterCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/UpdateRenter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult UpdateRenter([FromBody] UpdateRenterCommand updateRenterCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new RenterHandlers(_contractUnitOfWork);
            var result = handler.Handle(updateRenterCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/DeleteRenter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Delete([FromBody] DeleteRenterCommand deleteTenantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new RenterHandlers(_contractUnitOfWork);
            var result = handler.Handle(deleteTenantCommand);

            return Ok(result);
        }
    }
}
