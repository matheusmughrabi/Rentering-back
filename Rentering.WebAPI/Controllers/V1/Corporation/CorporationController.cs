﻿using Microsoft.AspNetCore.Authorization;
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
            if (command.Invalid)               
                return Ok(new CommandResult(false, "Corrija os problemas abaixo!", command.Notifications.ConvertCommandNotifications(), null));

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
    }
}

// Trocar invite para receber email e não id OK
// Criar convites pendentes OK
// Criar aceitar ou recusar convite OK

// Criar status da corporação OK
// Criar possibilidade de adicionar mês -> Só pode adicionar mês quando a corporação estiver ativa OK
// Uma vez que o contrato estiver com status Ativo, nenhum novo participante poderá entrar ou sair OK
// Implementar ParticipantBalance OK
// Implementar status de cada ParticipantBalance (pendente, aceito, recusado), implementar também comentários do ParticipantBalance
// Uma vez que todos os participantBalance de um mês forem aceitos o status do mês será colocado como Concluído (pendente, concluído, recusado) e será impossível alterar os dados

// Refatoração
// 1 - Utilizar enum result em todo o back end e implementar validador de enum OK
// 2 - Utilizar enum result no front end OK
// 3 - Separar models em request e result OK

// Enviar total profit correto ao criar novo mês OK
// Contestar mês OK
// O que deve acontecer ao contestar mês? -> Deverá ser possível aceitar se o usuário quiser OK


// Criar condições para mostrar ou não os botões OK
// Criar componentes no angular OK
// Acertar validação de commands tanto no back como no front OK
// Deletar telas não utilizadas OK
// Melhorar responsividade das telas -> Ícone de carregando, redirecionamentos corretos e botão de aceitar mês
// Melhorar telas
// Criar módulos


// CORREÇÕES
// Participante recusou participação, você deseja prosseguir com o contrato sem a participação dele?
// Corrigir quando não existir participante


// Próximas features
// 1- Descrição em participant balance
// 2 - Registrar cada lucro individual do mês e só então fechar o mês
// 3 - Criar página home
