using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;

namespace Rentering.WebAPI.Controllers.ContractContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractWithGuarantorController : RenteringBaseController
    {
        private readonly IContractWithGuarantorCUDRepository _contractWithGuarantorCUDRepository;
        private readonly IContractWithGuarantorQueryRepository _contractWithGuarantorQueryRepository;

        public ContractWithGuarantorController(
            IContractWithGuarantorCUDRepository contractWithGuarantorCUDRepository,
            IContractWithGuarantorQueryRepository contractWithGuarantorQueryRepository)
        {
            _contractWithGuarantorCUDRepository = contractWithGuarantorCUDRepository; 
            _contractWithGuarantorQueryRepository = contractWithGuarantorQueryRepository;
        }

        [HttpPost]
        [Route("v1/CreateContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateGuarantor([FromBody] CreateContractGuarantorCommand createContractGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new EstateContractGuarantorHandlers(_contractWithGuarantorCUDRepository, _contractWithGuarantorQueryRepository);
            var result = handler.Handle(createContractGuarantorCommand);

            return Ok(result);
        }
    }
}
