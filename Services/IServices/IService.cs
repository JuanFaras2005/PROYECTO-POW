using Domain;
using Services.Dtos;

namespace Services.IServices
{
    public interface IService
    {
        Task AddMilk(int v, DateTime now);
        Task CrearCurso(Curso curso);
        Task<IList<Curso>> GetAllCourses();
        Task<List<MilkModel>> GetAllMilks();
        Task<Curso> GetCursoPorId(int id);
        Task EditarCurso(Curso curso);
        Task EliminarCurso(int id);
        Task<List<Profesor>> GetAllProfesores();
        Task<bool> EstaInscrito(string estudianteId, int cursoId);
        Task InscribirEstudiante(string estudianteId, int cursoId);
    }
}
