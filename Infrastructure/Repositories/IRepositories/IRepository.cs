using Domain;

namespace Infrastructure.Repositories.IRepositories
{
    public interface IRepository
    {
        Task AddMilk(Milk milk);
        Task<List<Milk>> GetAllMilks();

        Task<IList<Curso>> GetAllCursos(); 
        Task<Curso> GetCursoPorId(int id);

        Task<List<Profesor>> GetAllProfesores();

        Task AddCurso(Curso curso);
        Task<bool> EstaInscrito(string estudianteId, int cursoId);
        Task AgregarInscripcion(string estudianteId, int cursoId);
    }
}
