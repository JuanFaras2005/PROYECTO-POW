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

        public async Task EditarCurso(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddMilk(int v, DateTime now)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MilkModel>> GetAllMilks()
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetAllProfesores()
        {
            throw new NotImplementedException();
        }

        Task<List<Profesor>> IService.GetAllProfesores()
        {
            throw new NotImplementedException();
        }
    }
}
