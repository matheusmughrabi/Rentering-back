using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.Handlers;
using Rentering.Contracts.Domain.Data;
using System.Linq;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractWithGuarantorController : RenteringBaseController
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public ContractWithGuarantorController(
            IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        [HttpGet]
        [Route("v1/Contract/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractById(int id)
        {
            var result = _contractUnitOfWork.ContractWithGuarantorQuery.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("v1/GetRenterContractsOfCurrentUser")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetRenterContractsOfCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contracts = _contractUnitOfWork.ContractWithGuarantorQuery.GetAll();
            var renters = _contractUnitOfWork.RenterQuery.GetRenterProfilesOfCurrentUser(accountId);

            var contractsOfCurrentUser = contracts.Where(c => renters.Any(r => r.Id == c.RenterId)).ToList();

            return Ok(contractsOfCurrentUser);
        }

        [HttpGet]
        [Route("v1/GetTenantContractsOfCurrentUser")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetTenantContractsOfCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contracts = _contractUnitOfWork.ContractWithGuarantorQuery.GetAll();
            var tenants = _contractUnitOfWork.TenantQuery.GetTenantProfilesOfCurrentUser(accountId);

            var contractsOfCurrentUser = contracts.Where(c => tenants.Any(r => r.Id == c.TenantId)).ToList();

            return Ok(contractsOfCurrentUser);
        }

        [HttpGet]
        [Route("v1/GetGuarantorContractsOfCurrentUser")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetGuarantorContractsOfCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contracts = _contractUnitOfWork.ContractWithGuarantorQuery.GetAll();
            var guarantors = _contractUnitOfWork.GuarantorQuery.GetGuarantorProfilesOfCurrentUser(accountId);

            var contractsOfCurrentUser = contracts.Where(c => guarantors.Any(r => r.Id == c.GuarantorId)).ToList();

            return Ok(contractsOfCurrentUser);
        }

        [HttpPost]
        [Route("v1/CreateContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContract([FromBody] CreateContractGuarantorCommand createContractGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork);
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

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork);
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

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork);
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

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork);
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

            var handler = new ContractGuarantorHandlers(_contractUnitOfWork);
            var result = handler.Handle(createContractPaymentCycleCommand);

            return Ok(result);
        }
    }
}
