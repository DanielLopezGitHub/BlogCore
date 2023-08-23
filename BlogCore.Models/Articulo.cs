using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es Obligatorio")]
        [Display(Name = "Nombre del Articulo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public string? FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string? UrlImagen { get; set; }

        // Crear Relacion 1-Many entre Categoria y Articulo, una Categoria se le puede asignar a Muchos Articulos, pero cada Articulo va a poder tener
        // una sola Categoria, y dicha Categoria se va a guardar en este campo.
        [Required]
        public int CategoriaId { get; set; }

        // Invocaremos al Modelo Categoria e indicamos a su llave foranea cual es su Propiedad en la que se guardara.
        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; }

        // NOTA:
        // Siempre al crear nuevos modelos, lo inmediatamente siguiente es ir a agregarlos al ApplicationDbContext, luego crear su IRepository y su
        // Repository, agregarlos al IContenedorTrabajo y ContenedorTrabajo y luego ejecutar la migracion.
    }
}
