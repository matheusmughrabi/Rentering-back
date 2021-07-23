using Microsoft.AspNetCore.Mvc;
using System;

namespace Rentering.WebAPI.Controllers
{
    public class RenteringBaseController : ControllerBase
    {
        protected const string authenticatedUserMessage = "Usuário autenticado é invalido";

        protected int GetCurrentUserId()
        {
            var isParsingSuccesful = int.TryParse(User.Identity.Name, out int accountId);

            if (isParsingSuccesful == false)
                throw new Exception("Usuário logado é inválido.");

            return accountId;
        }
    }
}
