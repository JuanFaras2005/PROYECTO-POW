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
    }
}
