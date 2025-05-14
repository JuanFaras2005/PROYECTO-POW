using AutoMapper;
using Domain;
using Infrastructure.Repositories.IRepositories;
using Services.Dtos;
using Services.IServices;

namespace Services.Services
{
    public class Service : IService
    {
        private IRepository Repository { get; set; }
        private IMapper Mapper { get; set; }

        public Service(IRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
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

        public Task<dynamic> GetAllProfesores()
        {
            throw new NotImplementedException();
        }
    }

}
