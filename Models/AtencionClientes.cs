using System.ComponentModel.DataAnnotations;

namespace TodoParaTuPerro.Models
{
    public class AtencionClientes
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string NombreNome { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Telefono { get; set; }
    }
}
