using Microsoft.AspNetCore.Mvc;

namespace Rentering.WebAPI.Controllers.Error
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : RenteringBaseController
    {
        [HttpGet]
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
