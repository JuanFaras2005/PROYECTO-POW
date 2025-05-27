using AutoMapper;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class Service : IService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Service(Infrastructure.Repositories.IRepositories.IRepository @object, IMapper mapper, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddMilk(int liters, DateTime dateTime)
        {
            var m = new Milk { Liters = liters, RecolectionDate = dateTime };
            _context.Milks.Add(m);
            await _context.SaveChangesAsync();
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
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
        }

        public async Task EditarCurso(Curso curso)
        {
            var existente = await _context.Cursos.FindAsync(curso.Id);
            if (existente != null)
            {
                existente.Nombre = curso.Nombre;
                existente.Descripcion = curso.Descripcion;
                existente.ProfesorId = curso.ProfesorId;
                existente.FechaInicio = curso.FechaInicio;
                existente.FechaFin = curso.FechaFin;
                _context.Cursos.Update(existente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarCurso(int id)
        {
            var c = await _context.Cursos.FindAsync(id);
            if (c != null)
            {
                _context.Cursos.Remove(c);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ApplicationUser>> GetAllProfesores()
        {
            // Devuelve todos los usuarios; si quieres filtrar solo rol "Profesor", lo haces aquí.
            return await _userManager.GetUsersInRoleAsync("Profesor") as List<ApplicationUser>;
        }

        public Task<List<MilkModel>> GetAllMilks()
        {
            throw new NotImplementedException();
        }
    }
}
