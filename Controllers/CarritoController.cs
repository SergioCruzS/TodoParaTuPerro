﻿using Microsoft.AspNetCore.Mvc;

namespace TodoParaTuPerro.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Carrito()
        {
            return View();
        }
    }
}
