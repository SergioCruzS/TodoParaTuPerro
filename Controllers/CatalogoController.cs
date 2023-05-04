using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Catalogo()
        {
            return View();
        }
    }
}
