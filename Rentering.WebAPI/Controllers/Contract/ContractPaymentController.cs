using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractPaymentController : ControllerBase
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public ContractPaymentController(
            IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }

        [HttpGet]
        [Route("v1/Payment/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPaymentById(int id)
        {
            var result = _contractUnitOfWork.ContractPaymentQuery.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("v1/GetPaymentsOfContract")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPaymentsOfContract(int contractId)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            // TODO - GetOfCurrentUser()
            var result = _contractUnitOfWork.ContractPaymentQuery.GetAll().Where(c => c.ContractId == contractId);

            return Ok(result);
        }
    }
}
