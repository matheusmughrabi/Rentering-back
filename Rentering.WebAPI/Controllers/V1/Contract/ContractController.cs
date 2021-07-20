using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.Handlers;
using Rentering.Contracts.Domain.Data;

namespace Rentering.WebAPI.Controllers.V1.Contract
{
    [Route("api/v1/contracts")]
    [ApiController]
    public class ContractController : RenteringBaseController
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public ContractController(IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        #region GetContractsOfCurrentUser
        [HttpGet]
        [Route("UserContracts")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractsOfCurrentUser()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            var contracts = _contractUnitOfWork.ContractQueryRepository.GetContractsOfCurrentUser(accountId);

            return Ok(contracts);
        }
        #endregion

        #region GetContractDetailed
        [HttpGet]
        [Route("ContractDetailed/{contractId}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractDetailed(int contractId)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            var contract = _contractUnitOfWork.ContractQueryRepository.GetContractDetailed(contractId);

            //if (contract.Participants.Where(c => c.AccountId == accountId).Count() == 0)
            //    return BadRequest("You are not a participant of this contract");

            return Ok(contract);
        }
        #endregion

        #region GetPendingInvitations
        [HttpGet]
        [Route("PendingInvitations")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPendingInvitations()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            var pendingInvitations = _contractUnitOfWork.ContractQueryRepository.GetPendingInvitations(accountId);

            return Ok(pendingInvitations);
        }
        #endregion

        #region GetPaymentsOfContract
        [HttpGet]
        [Route("PaymentsOfContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPaymentsOfContract(int contractId)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            var result = _contractUnitOfWork.ContractQueryRepository.GetPaymentsOfContract(contractId);

            return Ok(result);
        }
        #endregion

        #region CreateContract
        [HttpPost]
        [Route("CreateContract")]
        //[Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContract([FromBody] CreateContractCommand createContractGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            createContractGuarantorCommand.AccountId = accountId;

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(createContractGuarantorCommand);

            return Ok(result);
        }
        #endregion

        #region InviteParticipant
        [HttpPut]
        [Route("InviteParticipant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult InviteParticipant([FromBody] InviteParticipantCommand inviteParticipantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            inviteParticipantCommand.CurrentUserId = accountId;

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(inviteParticipantCommand);

            return Ok(result);
        }
        #endregion

        #region RemoveParticipant
        [HttpPut]
        [Route("RemoveParticipant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RemoveParticipant([FromBody] RemoveParticipantCommand removeParticipantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            removeParticipantCommand.CurrentUserId = accountId;

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(removeParticipantCommand);

            return Ok(result);
        }
        #endregion

        # region AcceptToParticipate
        [HttpPatch]
        [Route("AcceptToParticipate")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptToParticipate([FromBody] AcceptToParticipateCommand acceptToParticipateCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            acceptToParticipateCommand.AccountId = accountId;

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(acceptToParticipateCommand);

            return Ok(result);
        }
        #endregion

        #region RejectToParticipate
        [HttpPatch]
        [Route("RejectToParticipate")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectToParticipate([FromBody] RejectToParticipateCommand rejectToParticipateCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            rejectToParticipateCommand.AccountId = accountId;

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(rejectToParticipateCommand);

            return Ok(result);
        }
        #endregion

        #region CalculateCurrentOwedAmount
        [HttpPost]
        [Route("CalculateOwedAmount")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CalculateCurrentOwedAmount([FromBody] GetCurrentOwedAmountCommand getCurrentOwedAmountCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            getCurrentOwedAmountCommand.CurrentUserId = accountId;

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(getCurrentOwedAmountCommand);

            return Ok(result);
        }
        #endregion

        #region ExecutePayment
        [HttpPatch]
        [Route("ExecutePayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult ExecutePayment([FromBody] ExecutePaymentCommand executePaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(executePaymentCommand);

            return Ok(result);
        }
        #endregion

        #region AcceptPayment
        [HttpPatch]
        [Route("AcceptPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptPayment([FromBody] AcceptPaymentCommand acceptPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(acceptPaymentCommand);

            return Ok(result);
        }
        #endregion

        #region RejectPayment
        [HttpPatch]
        [Route("RejectPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectPayment([FromBody] RejectPaymentCommand rejectPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest(authenticatedUserMessage);

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(rejectPaymentCommand);

            return Ok(result);
        }
        #endregion
    }
}
