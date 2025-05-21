using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Models.ViewModels.Estudiantes
{
    public class EstudianteEditarViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        
        [Required]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
