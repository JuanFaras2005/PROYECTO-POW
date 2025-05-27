using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Models.ViewModels.Curso
{
    public class CursoEditarViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del curso")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Descripción del curso")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El profesor es obligatorio.")]
        [Display(Name = "Profesor")]
        public string ProfesorId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
