using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es Obligatorio")]
        [Display(Name = "Nombre del Slider")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es obligatorio seleccionar una Imagen")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Slider")]
        public string UrlImagen { get; set; }

        [Required]
        public bool Estado { get; set; }

        // NOTA:
        // Siempre al crear nuevos modelos, lo inmediatamente siguiente es ir a agregarlos al ApplicationDbContext, luego crear su IRepository y su
        // Repository, agregarlos al IContenedorTrabajo y ContenedorTrabajo y luego ejecutar la migracion.
    }
}
