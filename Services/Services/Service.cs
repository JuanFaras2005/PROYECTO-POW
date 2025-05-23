using AutoMapper;
using Domain;
using Infrastructure.Repositories;
using Infrastructure.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class Service : IService
    {
        private readonly IRepository Repository;
        private readonly IMapper Mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public Service(IRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            Repository = repository;
            Mapper = mapper;
            _userManager = userManager;
            _context = context;
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
            var users = await _userManager.Users.ToListAsync();
            var profesores = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Profesor"))
                {
                    profesores.Add(new ApplicationUser
                    {
                        Id = user.Id,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido
                    });
                }
            }

            return profesores;
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
