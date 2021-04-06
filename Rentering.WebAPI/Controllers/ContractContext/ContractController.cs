using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Authorization.CommandHandlers;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.QueryResults;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;

namespace Rentering.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : RenteringBaseController
    {
        private readonly IContractCUDRepository _contractCUDRepository;
        private readonly IContractQueryRepository _contractQueryRepository;
        private readonly IContractAuthRepository _contractAuthRepository;
        private readonly IAuthContractService _contractService;

        public ContractController(IContractCUDRepository contractCUDRepository, IContractQueryRepository contractQueryRepository, IContractAuthRepository contractAuthRepository, IAuthContractService contractService)
        {
            _contractCUDRepository = contractCUDRepository;
            _contractQueryRepository = contractQueryRepository;
            _contractAuthRepository = contractAuthRepository;
            _contractService = contractService;
        }

        [HttpGet]
        [Route("v1/Contracts/{id}")]
        [Authorize(Roles = "Admin")]
        public GetContractQueryResult GetContract(int id)
        {
            var result = _contractQueryRepository.GetContractById(id);

            return result;
        }

        [HttpGet]
        [Route("v1/Contracts/Renter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractsOfRenter()
        {          
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contractAuthQueryResult = _contractAuthRepository.GetContractUserProfileIdOfTheCurrentUser(accountId);
            var contractProfileUserId = contractAuthQueryResult.Id;            

            var result = _contractQueryRepository.GetContractsOfRenter(contractProfileUserId);

            return Ok(result);
        }

        [HttpGet]
        [Route("v1/Contracts/Tenant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractsOfTentant()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contractAuthQueryResult = _contractAuthRepository.GetContractUserProfileIdOfTheCurrentUser(accountId);
            var contractProfileUserId = contractAuthQueryResult.Id;

            var result = _contractQueryRepository.GetContractsOfTenant(contractProfileUserId);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/Contracts/CreateContractAsRenter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContractAsRenter([FromBody] CreateContractCommand contractCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contractAuthQueryResult = _contractAuthRepository.GetContractUserProfileIdOfTheCurrentUser(accountId);
            var contractProfileUserId = contractAuthQueryResult.Id;

            var authRenterCommand = new AuthRenterCommand(contractProfileUserId);
            var authHandler = new AuthContractHandlers(_contractService);
            var authResult = authHandler.Handle(authRenterCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            contractCommand.RenterId = contractProfileUserId;
            var handler = new ContractHandlers(_contractCUDRepository);
            var result = handler.Handle(contractCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("v1/Contracts/UpdateRentPrice")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult ChangeRentPrice([FromBody] UpdateRentPriceCommand contractCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthContractRenterCommand(contractCommand.Id, authenticatedUserId);
            var authHandler = new AuthContractHandlers(_contractService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new ContractHandlers(_contractCUDRepository);
            var result = handler.Handle(contractCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/Contracts")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Delete([FromBody] DeleteContractCommand contractCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthContractRenterCommand(contractCommand.Id, authenticatedUserId);
            var authHandler = new AuthContractHandlers(_contractService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new ContractHandlers(_contractCUDRepository);
            var result = handler.Handle(contractCommand);

            return Ok(result);
        }
    }
}
