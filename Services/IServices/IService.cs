using Domain;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        Task<List<ApplicationUser>> GetAllProfesores();

        //Task<bool> EstaInscrito(string estudianteId, int cursoId);
        //Task InscribirEstudiante(string estudianteId, int cursoId);
    }
}

