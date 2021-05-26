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
        [Authorize(Roles = "RegularUser,Admin")]
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
    }
}
