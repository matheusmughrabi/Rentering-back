using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Authorization.CommandHandlers;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories;
using Rentering.Contracts.Domain.Services;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuarantorController : ControllerBase
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;
        private readonly IAuthGuarantorService _authGuarantorService;

        public GuarantorController(
            IContractUnitOfWork contractUnitOfWork, 
            IAuthGuarantorService authGuarantorService)
        {
            _contractUnitOfWork = contractUnitOfWork;
            _authGuarantorService = authGuarantorService;
        }



        [HttpGet]
        [Route("v1/Guarantors/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetGuarantorById(int id)
        {
            var result = _contractUnitOfWork.GuarantorQuery.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("v1/Guarantors")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetGuarantorProfilesOfCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var result = _contractUnitOfWork.GuarantorQuery.GetGuarantorProfilesOfCurrentUser(accountId);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/CreateGuarantor")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateGuarantor([FromBody] CreateGuarantorCommand createGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            createGuarantorCommand.AccountId = accountId;

            var handler = new GuarantorHandlers(_contractUnitOfWork);
            var result = handler.Handle(createGuarantorCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/UpdateGuarantor")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult UpdateTenant([FromBody] UpdateGuarantorCommand updateGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authGuarantorCommand = new AuthCurrentUserAndProfileGuarantorMatchCommand(accountId, updateGuarantorCommand.Id);
            var authHandler = new AuthGuarantorHandlers(_authGuarantorService);
            var authResult = authHandler.Handle(authGuarantorCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            updateGuarantorCommand.AccountId = accountId;

            var handler = new GuarantorHandlers(_contractUnitOfWork);
            var result = handler.Handle(updateGuarantorCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/DeleteGuarantor")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult DeleteGuarantor([FromBody] DeleteGuarantorCommand deleteGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authGuarantorCommand = new AuthCurrentUserAndProfileGuarantorMatchCommand(authenticatedUserId, deleteGuarantorCommand.Id);
            var authHandler = new AuthGuarantorHandlers(_authGuarantorService);
            var authResult = authHandler.Handle(authGuarantorCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new GuarantorHandlers(_contractUnitOfWork);
            var result = handler.Handle(deleteGuarantorCommand);

            return Ok(result);
        }
    }
}
