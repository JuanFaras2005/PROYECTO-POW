using System.ComponentModel.DataAnnotations;

namespace MvcTemplate.Models
{
    public class ProfesorViewModel
    {
        public string Id { get; set; } // Para editar y eliminar

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string Email { get; set; }

        // Para creación es obligatorio, para edición opcional (manejado en controlador)
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria al crear", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
