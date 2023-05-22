using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TodoParaTuPerro.Data;
using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task <IActionResult> Index(UsuarioModel _usuario)
        {
            DA_Logica _da_usuario=new DA_Logica(); //Crear instancia de la clase (Objeto)

            var usuario = _da_usuario.ValidarUsuario(_usuario.Correo, _usuario.Clave);  //Variable para almacenar el uruario en caso de que se encuentre

            if (usuario != null)
            {
                var claims = new List<Claim> //Crear Cookie de autorización/sesión
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("Correo",usuario.Correo)
                };

                //Añadir roles para el usuario
                //Recorremos los roles que ya teniamos dentro de la propiedad de usuario
                foreach(string rol in usuario.Roles) //usuario.Roles es un Array
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                //Creamos la Cookie para el acceso
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)); //con claimsIdentity le pasamos todo el esquema del usuario (Nombres, Correos y Roles) 

                if (usuario.Correo.EndsWith("@TPTP.compras.org"))
                {
                    return RedirectToAction("Compras", "Compras");
                }

                return RedirectToAction("Index","Home");



            }
            else
            {
                return View("Login");

            }
        }

        //Metodo de cerrar sesión
        public async Task<IActionResult> Salir()
        {
            //Eliminar la Cookie de sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }

}
