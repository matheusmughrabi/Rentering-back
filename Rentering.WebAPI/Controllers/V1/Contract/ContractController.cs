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
            var contracts = _contractUnitOfWork.ContractQueryRepository.GetContractsOfCurrentUser(GetCurrentUserId());

            return Ok(contracts);
        }
        #endregion

        #region GetContractDetailed
        [HttpGet]
        [Route("ContractDetailed/{contractId}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetContractDetailed(int contractId)
        {
            var contract = _contractUnitOfWork.ContractQueryRepository.GetContractDetailed(GetCurrentUserId(), contractId);

            return Ok(contract);
        }
        #endregion

        #region GetPendingInvitations
        [HttpGet]
        [Route("PendingInvitations")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPendingInvitations()
        {
            var pendingInvitations = _contractUnitOfWork.ContractQueryRepository.GetPendingInvitations(GetCurrentUserId());

            return Ok(pendingInvitations);
        }
        #endregion

        #region GetPaymentsOfContract
        [HttpGet]
        [Route("PaymentsOfContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPaymentsOfContract(int contractId)
        {
            var result = _contractUnitOfWork.ContractQueryRepository.GetPaymentsOfContract(GetCurrentUserId());

            return Ok(result);
        }
        #endregion

        #region CreateContract
        [HttpPost]
        [Route("CreateContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContract([FromBody] CreateContractCommand createContractGuarantorCommand)
        {
            createContractGuarantorCommand.AccountId = GetCurrentUserId();

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
            inviteParticipantCommand.CurrentUserId = GetCurrentUserId();

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
            removeParticipantCommand.CurrentUserId = GetCurrentUserId();

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
            acceptToParticipateCommand.AccountId = GetCurrentUserId();

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
            rejectToParticipateCommand.AccountId = GetCurrentUserId();

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(rejectToParticipateCommand);

            return Ok(result);
        }
        #endregion

        #region ActivateContract
        [HttpPatch]
        [Route("Activate")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Activate([FromBody] ActivateContractCommand activateContractCommand)
        {
            activateContractCommand.CurrentUserId = GetCurrentUserId();

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(activateContractCommand);

            return Ok(result);
        }
        #endregion

        #region CalculateCurrentOwedAmount
        [HttpPost]
        [Route("CalculateOwedAmount")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CalculateCurrentOwedAmount([FromBody] GetCurrentOwedAmountCommand getCurrentOwedAmountCommand)
        {
            getCurrentOwedAmountCommand.CurrentUserId = GetCurrentUserId();

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
            executePaymentCommand.CurrentUserId = GetCurrentUserId();

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
            acceptPaymentCommand.CurrentUserId = GetCurrentUserId();

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
            rejectPaymentCommand.CurrentUserId = GetCurrentUserId();

            var handler = new ContractHandlers(_contractUnitOfWork);
            var result = handler.Handle(rejectPaymentCommand);

            return Ok(result);
        }
        #endregion
    }
}
