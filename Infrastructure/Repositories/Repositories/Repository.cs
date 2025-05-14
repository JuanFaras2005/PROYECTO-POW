using Domain;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Repositories
{
    public class Repository : BaseRepository, IRepository
    {
        public Repository(ApplicationDbContext _context) : base(_context)
        {
        }

        public async Task AddCurso(Curso curso)
        {
            try
            {
                Begin();
                context.Cursos.Add(curso); 
                await context.SaveChangesAsync(); 
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
                return await context.Cursos.ToListAsync();  
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
                return await context.Cursos.FindAsync(id);  
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
                context.Milks.Add(milk);
                await context.SaveChangesAsync();
                Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding milk", ex);
            }
        }

        public async Task<List<Milk>> GetAllMilks()
        {
            return await context.Milks.ToListAsync(); 
        }
    }
}
