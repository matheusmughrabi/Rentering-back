using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.Handlers;
using Rentering.Contracts.Domain.Data;

namespace Rentering.WebAPI.Controllers.V1.Contract
{
    [Route("api/v1/contracts")]
    [ApiController]
    public class EstateContractController : RenteringBaseController
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public EstateContractController(IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        [HttpGet]
        [Route("UserContracts")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractsOfCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contracts = _contractUnitOfWork.EstateContractQueryRepository.GetContractsOfCurrentUser(accountId);

            return Ok(contracts);
        }

        [HttpGet]
        [Route("ContractDetailed/{contractId}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractDetailed(int contractId)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var contract = _contractUnitOfWork.EstateContractQueryRepository.GetContractDetailed(contractId);

            //if (contract.Participants.Where(c => c.AccountId == accountId).Count() == 0)
            //    return BadRequest("You are not a participant of this contract");

            return Ok(contract);
        }

        [HttpGet]
        [Route("PendingInvitations")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPendingInvitations()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var pendingInvitations = _contractUnitOfWork.EstateContractQueryRepository.GetPendingInvitations(accountId);

            return Ok(pendingInvitations);
        }

        [HttpGet]
        [Route("PaymentsOfContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPaymentsOfContract(int contractId)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var result = _contractUnitOfWork.EstateContractQueryRepository.GetPaymentsOfContract(contractId);

            return Ok(result);
        }

        [HttpPost]
        [Route("CalculateOwedAmount")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CalculateCurrentOwedAmount([FromBody] GetCurrentOwedAmountCommand getCurrentOwedAmountCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            getCurrentOwedAmountCommand.CurrentUserId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(getCurrentOwedAmountCommand);

            return Ok(result);
        }              

        [HttpPost]
        [Route("CreateContract")]
        //[Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContract([FromBody] CreateEstateContractCommand createContractGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            createContractGuarantorCommand.AccountId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(createContractGuarantorCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("InviteParticipant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult InviteParticipant([FromBody] InviteParticipantCommand inviteParticipantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            inviteParticipantCommand.CurrentUserId = accountId;
            inviteParticipantCommand.ContractId = 1;

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(inviteParticipantCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreatePaymentCycle")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreatePaymentCycle([FromBody] CreatePaymentCycleCommand createPaymentCycleCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            createPaymentCycleCommand.CurrentUserId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(createPaymentCycleCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("ExecutePayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult ExecutePayment([FromBody] ExecutePaymentCommand executePaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(executePaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("AcceptPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptPayment([FromBody] AcceptPaymentCommand acceptPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(acceptPaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("RejectPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectPayment([FromBody] RejectPaymentCommand rejectPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(rejectPaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("AcceptToParticipate")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptToParticipate([FromBody] AcceptToParticipateCommand acceptToParticipateCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            acceptToParticipateCommand.AccountId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(acceptToParticipateCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("RejectToParticipate")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectToParticipate([FromBody] RejectToParticipateCommand rejectToParticipateCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            rejectToParticipateCommand.AccountId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(rejectToParticipateCommand);

            return Ok(result);
        }
    }
}
