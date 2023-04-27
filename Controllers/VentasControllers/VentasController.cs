using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers.VentasControllers
{
    public class VentasController : Controller
    {
        public IActionResult HomeVentas()
        {
            return View();
        }
        public IActionResult HistorialVentas()
        {
            return View();
        }
        public IActionResult Devoluciones()
        {
            return View();
        }
        public IActionResult AtencionClientes()
        {
            return View();
        }
    }
}
