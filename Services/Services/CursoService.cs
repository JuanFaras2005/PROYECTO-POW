using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CursoService : IService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inyecta UserManager en el constructor
        public CursoService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IList<Curso>> GetAllCourses()
        {
            return await _context.Cursos.Include(c => c.Profesor).ToListAsync();
        }

        public async Task<Curso> GetCursoPorId(int id)
        {
            return await _context.Cursos.Include(c => c.Profesor).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CrearCurso(Curso curso)
        {
            try
            {
                _context.Cursos.Add(curso);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error guardando curso: " + ex.Message);
                throw;
            }
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

        // Implementación para obtener todos los profesores (usuarios con rol Profesor)
        public async Task<List<ApplicationUser>> GetAllProfesores()
        {
            var profesores = await _userManager.GetUsersInRoleAsync("Profesor");
            return profesores.ToList();
        }

        // Métodos no implementados (puedes implementar o quitar si no usas)
        public Task AddMilk(int v, System.DateTime now)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MilkModel>> GetAllMilks()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EstaInscrito(string estudianteId, int cursoId)
        {
            throw new System.NotImplementedException();
        }

        public Task InscribirEstudiante(string estudianteId, int cursoId)
        {
            throw new System.NotImplementedException();
        }
    }
}
