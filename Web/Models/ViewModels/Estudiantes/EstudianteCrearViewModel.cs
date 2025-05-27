using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Models.ViewModels.Estudiantes
{
    public class EstudianteCrearViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
