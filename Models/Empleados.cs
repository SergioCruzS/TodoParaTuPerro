using System.ComponentModel.DataAnnotations;
namespace TodoParaTuPerro.Models
{
    public class Empleados
    {
        [Key]
        public int IdEmpleado { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida.")]
        public string Correo { get; set;}
        [Required]
        public string Rol { get; set;}
        [Required]
        public string Contrasena { get; set;}
        
    }
}
