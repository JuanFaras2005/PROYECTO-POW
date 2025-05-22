using System;
using System.Collections.Generic;
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

        [Required(ErrorMessage = "El profesor es obligatorio.")]
        public string ProfesorId { get; set; }

        public ApplicationUser Profesor { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}
