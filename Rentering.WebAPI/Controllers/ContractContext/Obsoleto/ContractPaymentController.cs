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
using System.Collections.Generic;

namespace Rentering.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractPaymentController : RenteringBaseController
    {
        private readonly IContractPaymentCUDRepository _contractPaymentCUDRepository;
        private readonly IContractPaymentQueryRepository _contractPaymentQueryRepository;
        private readonly IAuthContractService _contractService;

        public ContractPaymentController(IContractPaymentCUDRepository contractPaymentCUDRepository, IContractPaymentQueryRepository contractPaymentQueryRepository, IAuthContractService contractService)
        {
            _contractPaymentCUDRepository = contractPaymentCUDRepository;
            _contractPaymentQueryRepository = contractPaymentQueryRepository;
            _contractService = contractService;
        }

        [HttpGet]
        [Route("v1/GetContractPaymentsByContractId/{contracId}")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<GetContractPaymentsQueryResult> GetContractPaymentsByContractId(int contracId)
        {
            var result = _contractPaymentQueryRepository.GetContractPayments(contracId);

            return result;
        }

        [HttpPost]
        [Route("v1/CreatePaymentCycle")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreatePaymentCycle([FromBody] CreatePaymentCycleCommand contractPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthContractRenterCommand(contractPaymentCommand.ContractId, authenticatedUserId);
            var authHandler = new AuthContractHandlers(_contractService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new ContractPaymentHandlers(_contractPaymentCUDRepository);
            var result = handler.Handle(contractPaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("v1/ExecutePayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult ExecutePayment([FromBody] ExecutePaymentCommand contractPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthContractTenantCommand(contractPaymentCommand.ContractId, authenticatedUserId);
            var authHandler = new AuthContractHandlers(_contractService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new ContractPaymentHandlers(_contractPaymentCUDRepository);
            var result = handler.Handle(contractPaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("v1/AcceptPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptPayment([FromBody] AcceptPaymentCommand contractPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthContractRenterCommand(contractPaymentCommand.ContractId, authenticatedUserId);
            var authHandler = new AuthContractHandlers(_contractService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new ContractPaymentHandlers(_contractPaymentCUDRepository);
            var result = handler.Handle(contractPaymentCommand);

            return Ok(result);
        }

        [HttpPatch]
        [Route("v1/RejectPayment")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectPayment([FromBody] RejectPaymentCommand contractPaymentCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var authContractCommand = new AuthContractRenterCommand(contractPaymentCommand.ContractId, authenticatedUserId);
            var authHandler = new AuthContractHandlers(_contractService);
            var authResult = authHandler.Handle(authContractCommand);

            if (authResult.Success == false)
                return Unauthorized(authResult);

            var handler = new ContractPaymentHandlers(_contractPaymentCUDRepository);
            var result = handler.Handle(contractPaymentCommand);

            return Ok(result);
        }
    }
}
