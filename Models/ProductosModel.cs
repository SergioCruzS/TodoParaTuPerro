namespace TodoParaTuPerro.Models
{
    public class ProductosModel
    {
        public int Id_producto { get; set; }

        public string? Nombre { get; set; }

        public string? Marca { get; set; }

        public string? Descripcion { get; set; }

        public string? Imagen { get; set; }

        public int Stock { get; set; }

        public int Inventario { get; set; }

        public float Peso { get; set; }

        public float Precio { get; set; }

        public float Descuento { get; set; }

        public int Id_categoria { get; set; }
    }
}
