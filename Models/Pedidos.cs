using System.ComponentModel.DataAnnotations;

namespace TodoParaTuPerro.Models
{
    public class Pedidos
    {
        [Key]
        public int IdPedido { get; set; }
        [Required]
        public string FechaPedido { get; set; }
        [Required]
        public int ProductoId { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
        [Required]
        public decimal Total { get; set; }
        public string? NoGuia { get; set; }
        public string? Paqueteria { get; set; }
        public string? FechaEnvio { get; set; }
        public string? FechaEntrega { get; set; }
    }
}
