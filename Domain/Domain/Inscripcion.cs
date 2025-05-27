using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Inscripcion
    {
        public int Id { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public DateTime FechaInscripcion { get; set; }
    }
}
