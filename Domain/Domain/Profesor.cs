using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Profesor
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Curso> Cursos { get; set; }
    }
}
