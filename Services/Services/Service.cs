using AutoMapper;
using Domain;
using Infrastructure.Repositories.IRepositories;
using Services.Dtos;
using Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class Service : IService
    {
        private IRepository Repository { get; set; }
        private IMapper Mapper { get; set; }
        private UserManager<ApplicationUser> _userManager;

        public Service(IRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            Repository = repository;
            Mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<MilkModel>> GetAllMilks()
        {
            return Mapper.Map<List<MilkModel>>(await Repository.GetAllMilks());
        }

        public async Task AddMilk(int liters, DateTime dateTime)
        {
            Milk m = new Milk { Liters = liters, RecolectionDate = dateTime };
            await Repository.AddMilk(m);
        }

        public async Task<IList<Curso>> GetAllCourses()
        {
            return await Repository.GetAllCursos();
        }

        public async Task<Curso> GetCursoPorId(int id)
        {
            return await Repository.GetCursoPorId(id);
        }

        public async Task CrearCurso(Curso curso)
        {
            await Repository.AddCurso(curso);
        }

        public Task EditarCurso(Curso curso)
        {
            throw new NotImplementedException();
        }

        public Task EliminarCurso(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ApplicationUser>> GetAllProfesores()
        {
            var profesores = await _userManager.GetUsersInRoleAsync("Profesor");
            return profesores.ToList();
        }

        public Task<bool> EstaInscrito(string estudianteId, int cursoId)
        {
            throw new NotImplementedException();
        }

        public Task InscribirEstudiante(string estudianteId, int cursoId)
        {
            throw new NotImplementedException();
        }
    }
}
