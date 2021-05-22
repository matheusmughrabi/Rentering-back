﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Contracts.Application.Commands;
using Rentering.Contracts.Application.Handlers;
using Rentering.Contracts.Domain.Data;

namespace Rentering.WebAPI.Controllers.Contract
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuarantorController : ControllerBase
    {
        private readonly IContractUnitOfWork _contractUnitOfWork;

        public GuarantorController(
            IContractUnitOfWork contractUnitOfWork)
        {
            _contractUnitOfWork = contractUnitOfWork;
        }



        [HttpGet]
        [Route("v1/Guarantors/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetGuarantorById(int id)
        {
            var result = _contractUnitOfWork.GuarantorQuery.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("v1/CreateGuarantor")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CreateGuarantor([FromBody] CreateGuarantorCommand createGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new GuarantorHandlers(_contractUnitOfWork);
            var result = handler.Handle(createGuarantorCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("v1/UpdateGuarantor")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult UpdateTenant([FromBody] UpdateGuarantorCommand updateGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new GuarantorHandlers(_contractUnitOfWork);
            var result = handler.Handle(updateGuarantorCommand);

            return Ok(result);
        }

        [HttpDelete]
        [Route("v1/DeleteGuarantor")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult DeleteGuarantor([FromBody] DeleteGuarantorCommand deleteGuarantorCommand)
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int authenticatedUserId);

            if (isParsingSuccesful == false)
                return BadRequest("Invalid logged in user");

            var handler = new GuarantorHandlers(_contractUnitOfWork);
            var result = handler.Handle(deleteGuarantorCommand);

            return Ok(result);
        }
    }
}