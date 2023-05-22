using Microsoft.AspNetCore.Mvc;
using TodoParaTuPerro.Controllers.Datos;
using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Controllers.PaqueteriaControllers
{
    public class PaqueteriaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PaqueteriaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Pedidos()
        {
            var listaPedidos = _db.Pedidos.Where(pedidos => pedidos.NoGuia == null || pedidos.Paqueteria == null || pedidos.FechaEnvio == null).ToList();
            var listaPaqueterias = _db.Paqueterias.ToList();
            var modelo = new PedidosPaqueteriaModel
            {
                ListaPedidos = listaPedidos,
                ListaPaqueterias = listaPaqueterias
            };
            
            return View(listaPedidos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarGuia(Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                var pedidoExistente = _db.Pedidos.Find(pedidos.IdPedido);

                if (pedidoExistente != null)
                {
                    pedidoExistente.NoGuia = pedidos.NoGuia;
                    pedidoExistente.Paqueteria = pedidos.Paqueteria;
                    pedidoExistente.FechaEnvio = pedidos.FechaEnvio;
                    _db.Pedidos.Update(pedidoExistente);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Pedidos));
        }

        public IActionResult ActualizarEntrega(Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                var pedidoExistente = _db.Pedidos.Find(pedidos.IdPedido);

                if (pedidoExistente != null)
                {
                    pedidoExistente.FechaEntrega = pedidos.FechaEntrega;
                    _db.Pedidos.Update(pedidoExistente);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(PedidosEnviados));
        }

        public IActionResult PedidosEnviados()
        {
            var lista = _db.Pedidos.Where(pedidos => pedidos.FechaEntrega == null && pedidos.NoGuia != null).ToList();
            return View(lista);
        }

        public IActionResult PedidosCompletados()
        {
            var lista = _db.Pedidos.Where(pedidos => pedidos.FechaEntrega != null && pedidos.NoGuia != null).ToList();
            return View(lista);
        }

        public IActionResult Paqueterias()
        {
            var lista = _db.Paqueterias.ToList();
            return View(lista);
        }

        public IActionResult CrearPaqueteria(Paqueterias paqueterias)
        {
            if (ModelState.IsValid)
            {
                _db.Paqueterias.Add(paqueterias);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Paqueterias));
        }

        public IActionResult ActualizarPaqueteria(Paqueterias paqueterias)
        {
            if (ModelState.IsValid)
            {
                var paqueteriaExistente = _db.Paqueterias.Find(paqueterias.IdPaqueteria);

                if (paqueteriaExistente != null)
                {
                    paqueteriaExistente.NombrePaqueteria = paqueterias.NombrePaqueteria;
                    paqueteriaExistente.Direccion = paqueterias.Direccion;
                    paqueteriaExistente.CostoEnvio = paqueterias.CostoEnvio;
                    paqueteriaExistente.TiempoEntrega = paqueterias.TiempoEntrega;
                    _db.Paqueterias.Update(paqueteriaExistente);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Paqueterias));
        }

        public IActionResult EliminarPaqueteria(Paqueterias paqueterias)
        {

            var paqueteriaExistente = _db.Paqueterias.Find(paqueterias.IdPaqueteria);
            if (paqueteriaExistente != null)
            {
                _db.Paqueterias.Remove(paqueteriaExistente);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Paqueterias));
        }
    }
}
