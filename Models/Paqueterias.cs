using System.ComponentModel.DataAnnotations;

namespace TodoParaTuPerro.Models
{
    public class Paqueterias
    {
        [Key]
        public int IdPaqueteria { get; set; }
        [Required]
        public string NombrePaqueteria { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public decimal CostoEnvio { get; set; }
        [Required]
        public string TiempoEntrega { get; set; }
    }
}
