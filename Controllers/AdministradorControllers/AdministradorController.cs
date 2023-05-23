using Microsoft.AspNetCore.Mvc;
using TodoParaTuPerro.Controllers.Datos;
using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Controllers.AdministradorControllers
{
    public class AdministradorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdministradorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Empleados()
        {
            var listaEmpleados = _db.Empleados.Where(empleados => !empleados.Rol.Equals("Administrador")).ToList();
            return View(listaEmpleados);
        }

        [HttpPost]
        public IActionResult ActualizarEmpleado(Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                var empleadoExistente = _db.Empleados.Find(empleados.IdEmpleado);
                if (empleadoExistente != null)
                {
                    empleadoExistente.Nombre = empleados.Nombre;
                    empleadoExistente.Correo = empleados.Correo;
                    empleadoExistente.Rol = empleados.Rol;
                    _db.Empleados.Update(empleadoExistente);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Empleados));
        }

        public IActionResult EliminarEmpleado(Empleados empleados)
        {
            var empleadoExistente = _db.Empleados.Find(empleados.IdEmpleado);
            if (empleadoExistente != null)
            {
                _db.Empleados.Remove(empleadoExistente);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Empleados));
        }

        public IActionResult CrearEmpleado(Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                //var empleadoExistente = _db.Empleados.Where(empleado => empleado.Correo.Equals(empleados.Correo)).ToList();
                //if (empleadoExistente == null)
                //{
                    _db.Empleados.Add(empleados);
                    _db.SaveChanges();
                //}
            }
            return RedirectToAction(nameof(Empleados));
        }
    }
}
