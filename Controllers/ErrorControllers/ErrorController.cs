using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers.ErrorControllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("Error404");
                case 403:
                    return View("Error403");
                default:
                    return View("Error404");
            }
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error403()
        {
            return View();
        }
    }
}
