using System;
using System.ComponentModel.DataAnnotations;

namespace TodoParaTuPerro.Models
{
    public class CategoriasModel
    {
        public int Id_categoria { get; set; }

        [Required(ErrorMessage = "Escriba el nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Escriba una descripcion")]
        public string Descripcion { get; set; }
    }
}
