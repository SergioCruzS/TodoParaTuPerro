using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers.ErrorControllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
