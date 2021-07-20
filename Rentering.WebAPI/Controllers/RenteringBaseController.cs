using Microsoft.AspNetCore.Mvc;

namespace Rentering.WebAPI.Controllers
{
    public class RenteringBaseController : ControllerBase
    {
        protected const string authenticatedUserMessage = "Usuário autenticado é invalido";
    }
}
