using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers.ComprasControllers
{
    public class ComprasController : Controller
    {
        public IActionResult Compras()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }

        public IActionResult Categorias()
        {
            return View();
        }

        public IActionResult HacerCompra()
        {
            return View();
        }
    }
}
