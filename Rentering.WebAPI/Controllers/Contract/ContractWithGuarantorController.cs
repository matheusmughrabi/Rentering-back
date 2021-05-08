using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.CommandHandlers;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Domain.Repositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractWithGuarantorController : RenteringBaseController
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        private readonly IContractWithGuarantorQueryRepository _contractWithGuarantorQueryRepository;
        private readonly IRenterQueryRepository _renterQueryRepository;
        private readonly ITenantQueryRepository _tenantQueryRepository;
        private readonly IGuarantorQueryRepository _guarantorQueryRepository;
        private readonly IContractPaymentQueryRepository _contractPaymentQueryRepository;

        public ContractWithGuarantorController(
            IContractUnitOfWork contractUnitOfWork,
            IContractWithGuarantorQueryRepository contractWithGuarantorQueryRepository,
            IRenterQueryRepository renterQueryRepository,
            ITenantQueryRepository tenantQueryRepository,
            IGuarantorQueryRepository guarantorQueryRepository,
            IContractPaymentQueryRepository contractPaymentQueryRepository)
        {
            _contractUnitOfWork = contractUnitOfWork;

            _contractWithGuarantorQueryRepository = contractWithGuarantorQueryRepository;
            _renterQueryRepository = renterQueryRepository;
            _tenantQueryRepository = tenantQueryRepository;
            _guarantorQueryRepository = guarantorQueryRepository;
            _contractPaymentQueryRepository = contractPaymentQueryRepository;
        }

        [HttpPost]
        [Route("v1/CreateContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContract([FromBody] CreateContractGuarantorCommand createContractGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork, _contractWithGuarantorQueryRepository, _renterQueryRepository, _tenantQueryRepository, _guarantorQueryRepository,  _contractPaymentQueryRepository);
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

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork, _contractWithGuarantorQueryRepository, _renterQueryRepository, _tenantQueryRepository, _guarantorQueryRepository, _contractPaymentQueryRepository);
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

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork, _contractWithGuarantorQueryRepository, _renterQueryRepository, _tenantQueryRepository, _guarantorQueryRepository, _contractPaymentQueryRepository);
            var result = handler.Handle(inviteTenantToParticipateCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/InviteGuarantor")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult InviteGuarantor([FromBody] InviteGuarantorToParticipate inviteGuarantorToParticipate)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork, _contractWithGuarantorQueryRepository, _renterQueryRepository, _tenantQueryRepository, _guarantorQueryRepository, _contractPaymentQueryRepository);
            var result = handler.Handle(inviteGuarantorToParticipate);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/CreatePaymentCycle")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreatePaymentCycle([FromBody] CreateContractPaymentCycleCommand createContractPaymentCycleCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork, _contractWithGuarantorQueryRepository, _renterQueryRepository, _tenantQueryRepository, _guarantorQueryRepository, _contractPaymentQueryRepository);
            var result = handler.Handle(createContractPaymentCycleCommand);

            return Ok(result);
        }
    }
}
