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
        
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña (solo si desea cambiarla)")]
        public string Password { get; set; }

        public string Rol { get; set; } // Para mostrar el rol en la tabla
    }
}
