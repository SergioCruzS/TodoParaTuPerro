namespace TodoParaTuPerro.Models
{
    public class ComprasModel
    {
        public int Id_compra { get; set; }

        public DateTime? FechaCompra { get; set; }

        public string? DireccionEnvio { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public int CantidadArticulos { get; set; }

        public string? FormaPago { get; set; }

        public float PrecioArticulo { get; set; }

        public float TotalCompra { get; set; }

        public int NumAutorizacion { get; set; }

        public int Id_empleado { get; set; }

        public int Id_producto { get; set; }

        public int Id_proveedor { get; set; }
    }
}
