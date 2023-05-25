using System.ComponentModel.DataAnnotations;

namespace TodoParaTuPerro.Models
{
    public class Ventas
    {
        [Key]
        public int idDevoluciones { get; set; }
        [Required]
        public int idVenta { get; set; }
        [Required]
        public string Producto { get; set; }
        [Required]
        public string Marca { get; set;}
        [Required]
        public string Fecha { get; set;}
        [Required]
        public string idCliente { get; set; }
        [Required]
        public string Detalles { get; set;}
    }
}
