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

        #region GetCorporationDetailed
        [HttpGet]
        [Route("detailed/{id}")]
        [Authorize(Roles = "RegularUser,Admin")]
        public IActionResult GetCorporationDetailed(int id)
        {
            return Ok(_corporationUnitOfWork.CorporationQueryRepository.GetCorporationDetailed(GetCurrentUserId(), id));
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
            command.CurrentUserId = GetCurrentUserId();

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
            command.CurrentUserId = GetCurrentUserId();

            var handler = new CorporationHandlers(_corporationUnitOfWork);
            var result = handler.Handle(command);

            return Ok(result);

        }
        #endregion
    }
}

// Trocar invite para receber email e não id OK
// Criar convites pendentes OK
// Criar aceitar ou recusar convite OK

// Criar status da corporação OK
// Criar possibilidade de adicionar mês -> Só pode adicionar mês quando a corporação estiver ativa
// Uma vez que o contrato estiver com status Ativo, nenhum novo participante poderá entrar ou sair
