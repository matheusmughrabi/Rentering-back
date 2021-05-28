using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.ApplicationEF.Commands;
using Rentering.Contracts.ApplicationEF.Handlers;
using Rentering.Contracts.Domain.DataEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateContractEFController : RenteringBaseController
    {
        private readonly IContractUnitOfWorkEF _contractUnitOfWorkEF;

        public EstateContractEFController(IContractUnitOfWorkEF contractUnitOfWorkEF)
        {
            _contractUnitOfWorkEF = contractUnitOfWorkEF;
        }

        [HttpPost]
        [Route("v1/CreateContract")]
        //[Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateContract([FromBody] CreateEstateContractCommandEF createContractGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            createContractGuarantorCommand.AccountId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWorkEF);
            var result = handler.Handle(createContractGuarantorCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/InviteParticipant")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult InviteParticipant([FromBody] InviteParticipantCommandEF inviteParticipantCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            if (accountId == inviteParticipantCommand.ParticipantAccountId)
                return BadRequest("You cannot invite yourself to a contract");

            inviteParticipantCommand.CurrentUserId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWorkEF);
            var result = handler.Handle(inviteParticipantCommand);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/CreatePaymentCycle")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreatePaymentCycle([FromBody] CreatePaymentCycleCommandEF createPaymentCycleCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            createPaymentCycleCommand.CurrentUserId = accountId;

            var handler = new EstateContractHandlers(_contractUnitOfWorkEF);
            var result = handler.Handle(createPaymentCycleCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("v1/ExecutePayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult ExecutePayment([FromBody] ExecutePaymentCommandEF executePaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new EstateContractHandlers(_contractUnitOfWorkEF);
            var result = handler.Handle(executePaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("v1/AcceptPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptPayment([FromBody] AcceptPaymentCommandEF acceptPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new EstateContractHandlers(_contractUnitOfWorkEF);
            var result = handler.Handle(acceptPaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("v1/RejectPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectPayment([FromBody] RejectPaymentCommandEF rejectPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new EstateContractHandlers(_contractUnitOfWorkEF);
            var result = handler.Handle(rejectPaymentCommand);

            return Ok(result);
        }
    }
}
