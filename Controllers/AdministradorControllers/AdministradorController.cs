using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers.AdministradorControllers
{
    public class AdministradorController : Controller
    {
        public IActionResult Empleados()
        {
            return View();
        }
    }
}
