using System;
using System.ComponentModel.DataAnnotations;

namespace TodoParaTuPerro.Models
{
    public class ComprasModel
    {
        public int Id_compra { get; set; }

        public DateTime? FechaCompra { get; set; }

        //[Required(ErrorMessage = "Escriba la dirección de envío")]
        public string? DireccionEnvio { get; set; }

        public DateTime? FechaEntrega { get; set; }

        [Range(1, 999, ErrorMessage = "Cantidad invalida")]
        public int CantidadArticulos { get; set; }

        //[Required(ErrorMessage = "Escriba la forma de pago")]
        public string? FormaPago { get; set; }

        public float PrecioArticulo { get; set; }

        public float TotalCompra { get; set; }

        public int NumAutorizacion { get; set; }

        //[Required(ErrorMessage = "Escriba su ID")]
        [Range(1, 999, ErrorMessage = "ID Invalido")]
        public int Id_empleado { get; set; }

        public int Id_producto { get; set; }

        //[Required(ErrorMessage = "Escriba la ID del proveedor")]
        [Range(1, 999, ErrorMessage = "ID Invalido")]
        public int Id_proveedor { get; set; }
    }
}
