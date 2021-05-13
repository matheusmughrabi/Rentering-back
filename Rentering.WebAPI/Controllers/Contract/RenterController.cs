using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Application.Authorization.Handlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.Handlers;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Services;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenterController : RenteringBaseController
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;
        private readonly IAuthRenterService _authRenterService;

        public RenterController(
            IContractUnitOfWork contractUnitOfWork, 
            IAuthRenterService authRenterService)
        {
            _contractUnitOfWork = contractUnitOfWork;
            _authRenterService = authRenterService;
        }

        [HttpGet]
        [Route("v1/Renters/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetRenterById(int id)
        {
            var result = _contractUnitOfWork.RenterQuery.GetById(id);

            return Ok(result);
        }

        //[HttpGet]
        //[Route("v1/Renters")]
        //[Authorize(Roles = "RegularUser,Admin")]
        //public IActionResult GetRenterProfilesOfCurrentUser()
        //{
        //    var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

        //    if (isParsingSuccesful == false)
        //        return BadRequest("Invalid logged in user");

        //    var result = _contractUnitOfWork.RenterQuery.GetRenterProfilesOfCurrentUser(accountId);

        //    return Ok(result);
        //}

        [HttpPost]
        [Route("v1/CreateRenter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateRenter([FromBody] CreateRenterCommand createRenterCommand)
        {
            //var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            //if (isParsingSuccesful == false)
            //    return BadRequest("Invalid logged in user");

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

            //var authContractCommand = new AuthCurrentUserAndProfileRenterMatchCommand(accountId, updateRenterCommand.Id);
            //var authHandler = new AuthRenterHandlers(_authRenterService);
            //var authResult = authHandler.Handle(authContractCommand);

            //if (authResult.Success == false)
            //    return Unauthorized(authResult);

            //updateRenterCommand.AccountId = accountId;

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

            //var authContractCommand = new AuthCurrentUserAndProfileRenterMatchCommand(authenticatedUserId, deleteTenantCommand.Id);
            //var authHandler = new AuthRenterHandlers(_authRenterService);
            //var authResult = authHandler.Handle(authContractCommand);

            //if (authResult.Success == false)
            //    return Unauthorized(authResult);

            var handler = new RenterHandlers(_contractUnitOfWork);
            var result = handler.Handle(deleteTenantCommand);

            return Ok(result);
        }
    }
}
