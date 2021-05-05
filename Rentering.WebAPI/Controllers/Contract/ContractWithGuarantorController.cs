using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractWithGuarantorController : RenteringBaseController
    {
        private readonly IContractWithGuarantorCUDRepository _contractWithGuarantorCUDRepository;
        private readonly IContractWithGuarantorQueryRepository _contractWithGuarantorQueryRepository;
        private readonly IRenterCUDRepository _renterCUDRepository;
        private readonly IRenterQueryRepository _renterQueryRepository;
        private readonly ITenantCUDRepository _tenantCUDRepository;
        private readonly ITenantQueryRepository _tenantQueryRepository;

        public ContractWithGuarantorController(
            IContractWithGuarantorCUDRepository contractWithGuarantorCUDRepository,
            IContractWithGuarantorQueryRepository contractWithGuarantorQueryRepository,
            IRenterCUDRepository renterCUDRepository,
            IRenterQueryRepository renterQueryRepository, 
            ITenantCUDRepository tenantCUDRepository, 
            ITenantQueryRepository tenantQueryRepository)
        {
            _contractWithGuarantorCUDRepository = contractWithGuarantorCUDRepository;
            _contractWithGuarantorQueryRepository = contractWithGuarantorQueryRepository;
            _renterCUDRepository = renterCUDRepository;
            _renterQueryRepository = renterQueryRepository;
            _tenantCUDRepository = tenantCUDRepository;
            _tenantQueryRepository = tenantQueryRepository;
        }

        [HttpPost]
        [Route("v1/CreateContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContract([FromBody] CreateContractGuarantorCommand createContractGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new ContractGuarantorHandlers(_contractWithGuarantorCUDRepository, _contractWithGuarantorQueryRepository, _renterCUDRepository, _renterQueryRepository, _tenantCUDRepository, _tenantQueryRepository);
            var result = handler.Handle(createContractGuarantorCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/InviteRenter")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult InviteRenter([FromBody] InviteRenterToParticipate inviteRenterToParticipateCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new ContractGuarantorHandlers(_contractWithGuarantorCUDRepository, _contractWithGuarantorQueryRepository, _renterCUDRepository, _renterQueryRepository, _tenantCUDRepository, _tenantQueryRepository);
            var result = handler.Handle(inviteRenterToParticipateCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/InviteTenant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult InviteTenant([FromBody] InviteTenantToParticipate inviteTenantToParticipateCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new ContractGuarantorHandlers(_contractWithGuarantorCUDRepository, _contractWithGuarantorQueryRepository, _renterCUDRepository, _renterQueryRepository, _tenantCUDRepository, _tenantQueryRepository);
            var result = handler.Handle(inviteTenantToParticipateCommand);

            return Ok(result);
        }
    }
}
