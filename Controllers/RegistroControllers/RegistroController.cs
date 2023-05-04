using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers.RegistroControllers
{
    public class RegistroController : Controller
    {
        public IActionResult IniciarSesion()
        {
            return View();
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        public IActionResult RegistrarDatos()
        {
            return View();
        }

        public IActionResult OlvidarContraseña()
        {
            return View();
        }

        public IActionResult CodigoRecuperacion()
        {
            return View();
        }

        public IActionResult CambiarContraseña()
        {
            return View();
        }

        public IActionResult MensajeDeCambio()
        {
            return View();
        }

    }
}
