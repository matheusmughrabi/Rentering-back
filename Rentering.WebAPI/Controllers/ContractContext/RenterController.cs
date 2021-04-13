using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;

namespace Rentering.WebAPI.Controllers.ContractContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenterController : RenteringBaseController
    {
        private readonly IRenterCUDRepository _renterCUDRepository;

        public RenterController(IRenterCUDRepository renterCUDRepository)
        {
            _renterCUDRepository = renterCUDRepository;
        }

        [HttpPost]
        [Route("v1/CreateRenter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateRenter([FromBody] CreateRenterCommand createRenterCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            createRenterCommand.AccountId = accountId;

            var handler = new RenterCommandHandlers(_renterCUDRepository);
            var result = handler.Handle(createRenterCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/DeleteRenter/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Delete(int id)
        {
            _renterCUDRepository.DeleteRenter(id);

            var deletedRenter = new CommandResult(true, "Renter deleted successfuly", new
            {
                RenterId = id
            });

            return Ok(deletedRenter);
        }
    }
}
