using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("ProfesorId")]
        public ApplicationUser Profesor { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Nuevas propiedades para las fechas de inicio y fin
        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}
