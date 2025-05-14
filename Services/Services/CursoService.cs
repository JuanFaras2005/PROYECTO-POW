using Domain;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.IServices;

namespace Services.Services
{
    public class CursoService : IService
    {
        private readonly ApplicationDbContext _context;

        public CursoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Métodos para Cursos
        public async Task<IList<Curso>> GetAllCourses()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<Curso> GetCursoPorId(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task CrearCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
        }

        // Métodos temporales para Milk (por exigencia de la interfaz)
        public async Task AddMilk(int v, DateTime now)
        {
            throw new NotImplementedException(); // Lo puedes quitar luego
        }

        public async Task<List<MilkModel>> GetAllMilks()
        {
            throw new NotImplementedException(); // Lo puedes quitar luego
        }
    }
}
