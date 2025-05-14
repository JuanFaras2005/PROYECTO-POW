using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string? ProfesorId { get; set; } // ID del usuario profesor

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
