using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.QueryResults;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;

namespace Rentering.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractUserProfileController : RenteringBaseController
    {
        private readonly IContractUserProfileCUDRepository _userCUDRepository;
        private readonly IContractUserProfileQueryRepository _userQueryRepository;

        public ContractUserProfileController(IContractUserProfileCUDRepository userCUDRepository, IContractUserProfileQueryRepository userQueryRepository)
        {
            _userCUDRepository = userCUDRepository;
            _userQueryRepository = userQueryRepository;
        }

        [HttpGet]
        [Route("v1/ContractUserProfiles/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public GetContractUserProfileQueryResult GetContractUserProfile(int id)
        {
            var result = _userQueryRepository.GetUserById(id);

            return result;
        }

        [HttpGet]
        [Route("v1/ContractUserProfiles")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCurrentUserContractProfiles()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var result = _userQueryRepository.GetCurrentUserProfiles(accountId);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/ContractUserProfiles")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var createUserCommand = new CreateContractUserProfileCommand(accountId);

            var handler = new ContractUserProfileHandlers(_userCUDRepository);
            var result = handler.Handle(createUserCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/ContractUserProfiles/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public ICommandResult Delete(int id)
        {
            _userCUDRepository.DeleteContractUserProfile(id);

            var deletedUser = new CommandResult(true, "ContractUserProfile deleted successfuly", new
            {
                UserId = id
            });

            return deletedUser;
        }
    }
}

// GetCurrentUserContractProfiles()
