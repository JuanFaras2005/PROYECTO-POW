using Domain;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repositories
{
    public class Repository : BaseRepository, IRepository
    {
        public Repository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddCurso(Curso curso)
        {
            try
            {
                Begin();
                _context.Cursos.Add(curso);
                await _context.SaveChangesAsync();
                Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding course", ex);
            }
        }

        public async Task<IList<Curso>> GetAllCursos()
        {
            try
            {
                return await _context.Cursos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting all courses", ex);
            }
        }

        public async Task<Curso> GetCursoPorId(int id)
        {
            try
            {
                return await _context.Cursos
                    .Include(c => c.Profesor)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting course by ID", ex);
            }
        }

        public async Task AddMilk(Milk milk)
        {
            try
            {
                Begin();
                _context.Milks.Add(milk);
                await _context.SaveChangesAsync();
                Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding milk", ex);
            }
        }

        public async Task<List<Milk>> GetAllMilks()
        {
            return await _context.Milks.ToListAsync();
        }

        public async Task<List<Profesor>> GetAllProfesores()
        {
            return await _context.Profesores.ToListAsync();
        }

        public Task<bool> EstaInscrito(string estudianteId, int cursoId)
        {
            throw new NotImplementedException();
        }

        public Task AgregarInscripcion(string estudianteId, int cursoId)
        {
            throw new NotImplementedException();
        }
    }
}
