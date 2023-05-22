using System;
using System.ComponentModel.DataAnnotations;

namespace TodoParaTuPerro.Models
{
    public class ProductosModel: ValidationAttribute
    {
        public int Id_producto { get; set; }

        //[Required(ErrorMessage = "Escriba el nombre")]
        public string Nombre { get; set; }

        //[Required(ErrorMessage = "Escriba la marca")]
        public string Marca { get; set; }

        //[Required(ErrorMessage = "Escriba la descripción")]
        public string Descripcion { get; set; }

        //[Required(ErrorMessage = "Escriba la ubicación de la imagen")]
        //[DataType(DataType.ImageUrl, ErrorMessage = "No es una ruta de imagen")]
        public string Imagen { get; set; }

        [Range(0, 99999, ErrorMessage = "Cantidad invalida")]
        public int Stock { get; set; }
        //[DataType(((int)DataType.Custom))]

        [Range(0, 99999, ErrorMessage = "Cantidad invalida")]
        public int Inventario { get; set; }

        [Range(0, 100, ErrorMessage = "Peso no valido")]
        public float Peso { get; set; }

        [Range(0, 99999, ErrorMessage = "Precio no valido")]
        public float PrecioVenta { get; set; }

        [Range(1, 99999, ErrorMessage = "Precio no valido")]

        public float PrecioCompra { get; set; }

        //[Range(0,1, ErrorMessage = "Descuento no valido")]
        public float Descuento { get; set; }

        //[Required(ErrorMessage = "Escriba el ID de la categoría del producto")]
        public int Id_categoria { get; set; }


        //Datos para insertar compra en la Vista ComprarProductos
        public DateTime? FechaCompra { get; set; }

        public string? DireccionEnvio { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public int CantidadArticulos { get; set; }

        public string? FormaPago { get; set; }

        public float TotalCompra { get; set; }

        public int NumAutorizacion { get; set; }

        public int Id_empleado { get; set; }


        public int Id_proveedor { get; set; }


        //[MinLength(5, ErrorMessage ="Numero invalido")]
        public string NombreTarjeta { get; set; }

        //[MinLength(16, ErrorMessage = "Numero invalido")]
        //[Range(1000000000000000, 9999999999999999, ErrorMessage = "Numero invalido")]
        public int Tarjeta { get; set; }

        //[MinLength(5, ErrorMessage = "Numero invalido")]
        [Range(1000, 9999, ErrorMessage = "Numero invalido")]
        public int FechaTarjeta { get; set; }

        //[MinLength(3, ErrorMessage = "Numero invalido")]
        [Range(100, 999, ErrorMessage = "Numero invalido")]
        public int CVVTarjeta { get; set; }
    }
}