using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentering.Common.Shared.Commands;
using Rentering.Corporation.Application.Commands;
using Rentering.Corporation.Application.Handlers;
using Rentering.Corporation.Domain.Data;

namespace Rentering.WebAPI.Controllers.V1.Corporation
{
    [Route("api/v1/corporation")]
    [ApiController]
    public class CorporationController : RenteringBaseController
    {
        private readonly ICorporationUnitOfWork _corporationUnitOfWork;

        public CorporationController(ICorporationUnitOfWork corporationUnitOfWork)
        {
            _corporationUnitOfWork = corporationUnitOfWork;
        }

        #region GetCorporations
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCorporations()
        {
            return Ok(_corporationUnitOfWork.CorporationQueryRepository.GetCorporations(GetCurrentUserId()));
        }
        #endregion

        #region CreateCorporation
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Create([FromBody] CreateCorporationCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);
            
        }
        #endregion
    }
}
