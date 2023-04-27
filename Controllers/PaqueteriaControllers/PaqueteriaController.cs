using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers.PaqueteriaControllers
{
    public class PaqueteriaController : Controller
    {
        public IActionResult Pedidos()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }
    }
}
