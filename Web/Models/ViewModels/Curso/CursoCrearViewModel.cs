using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Models.ViewModels.Curso
{
    public class CursoCrearViewModel
    {
      
        [Required]
        [Display(Name = "Nombre del curso")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Descripción del curso")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El profesor es obligatorio.")]
        [Display(Name = "Profesor")]
        public string ProfesorId { get; set; }
    }
}
