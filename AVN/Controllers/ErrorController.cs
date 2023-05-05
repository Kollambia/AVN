using Microsoft.AspNetCore.Mvc;

namespace AVN.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/NotFound/{statusCode}")]
        public IActionResult NotFound(int statusCode) => View();
    }
}
