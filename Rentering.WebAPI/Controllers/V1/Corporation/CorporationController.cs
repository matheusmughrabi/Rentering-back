using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentering.Common.Shared.Commands;
using Rentering.Corporation.Application.Commands;
using Rentering.Corporation.Application.Handlers;
using Rentering.Corporation.Domain.Data;
using Rentering.Infra;
using System.Linq;

namespace Rentering.WebAPI.Controllers.V1.Corporation
{
    [Route("api/v1/corporation")]
    [ApiController]
    public class CorporationController : RenteringBaseController
    {
        private readonly ICorporationUnitOfWork _corporationUnitOfWork;
        private readonly RenteringDbContext _context;

        public CorporationController(ICorporationUnitOfWork corporationUnitOfWork, RenteringDbContext context)
        {
            _corporationUnitOfWork = corporationUnitOfWork;
            _context = context;
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

        #region GetCorporationDetailed
        [HttpGet]
        [Route("detailed/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCorporationDetailed(int id)
        {
            return Ok(_corporationUnitOfWork.CorporationQueryRepository.GetCorporationDetailed(GetCurrentUserId(), id));
        }
        #endregion

        #region GetPeriodDetailed
        [HttpGet]
        [Route("period/detailed/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetPeriodDetailed(int id)
        {
            return Ok(_corporationUnitOfWork.CorporationQueryRepository.GetPeriodDetailed(id));
        }
        #endregion

        #region GetInvitations
        [HttpGet]
        [Route("invitations")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetInvitations(int id)
        {
            return Ok(_corporationUnitOfWork.CorporationQueryRepository.GetInvitations(GetCurrentUserId()));
        }
        #endregion

        #region CreateCorporation
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult Create([FromBody] CreateCorporationCommand command)
        {
            if (command.Invalid)               
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            command.CurrentUserId = GetCurrentUserId();

            var license = _context.Account.AsNoTracking().Where(c => c.Id == command.CurrentUserId).Select(p => p.LicenseCode).FirstOrDefault();
            var numberOfCorporations = _context.Corporation.AsNoTracking().Where(c => c.AdminId == command.CurrentUserId).Count();

            if (license == 1 && numberOfCorporations >= 2)
            {
                var resultLicense = new CommandResult(false, "Impossível criar nova corporação", null, null);
                resultLicense.AddNotification("Você atingiu o limite de contratos para a licensa gratuita", "Licensa gratuita");
                return Ok(resultLicense);
            }

            if (license == 2 && numberOfCorporations >= 5)
            {
                var resultLicense = new CommandResult(false, "Impossível criar nova corporação", null, null);
                resultLicense.AddNotification("Você atingiu o limite de contratos para a licensa padrão", "Licensa padrão");
                return Ok(resultLicense);
            }

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

            
        }
        #endregion

        #region InviteParticipant
        [HttpPut]
        [Route("invite")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult InviteParticipant([FromBody] InviteToCorporationCommand command)
        {
            if (command.Invalid)
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region FinishCreation
        [HttpPut]
        [Route("finish-creation")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult FinishCreation([FromBody] FinishCreationCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region AcceptParticipation
        [HttpPut]
        [Route("participation/accept")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptParticipation([FromBody] AcceptParticipationInCorporationCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region RejectParticipation
        [HttpPut]
        [Route("participation/reject")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectParticipation([FromBody] RejectParticipationInCorporationCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region ActivateCorporation
        [HttpPut]
        [Route("activate")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult ActivateCorporation([FromBody] ActivateCorporationCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region AddMonth
        [HttpPut]
        [Route("add-month")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AddMonth([FromBody] AddMonthCommand command)
        {
            if (command.Invalid)
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region RegisterIncome
        [HttpPut]
        [Route("register-income")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RegisterIncome([FromBody] RegisterIncomeCommand command)
        {
            if (command.Invalid)
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region CloseMonth
        [HttpPut]
        [Route("close-month")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult CloseMonth([FromBody] CloseMonthCommand command)
        {
            if (command.Invalid)
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region AcceptBalance
        [HttpPut]
        [Route("balance/accept")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AcceptBalance([FromBody] AcceptBalanceCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region RejectBalance
        [HttpPut]
        [Route("balance/reject")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult RejectBalance([FromBody] RejectBalanceCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion

        #region AddParticipantDescriptionToMonth
        [HttpPut]
        [Route("participant-balance/description")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult AddParticipantDescriptionToMonth([FromBody] AddParticipantDescriptionToMonthCommand command)
        {
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion
    }
}