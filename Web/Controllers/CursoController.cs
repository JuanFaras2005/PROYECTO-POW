using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcTemplate.Models.ViewModels.Curso;
using Services.IServices;

namespace MvcTemplate.Controllers
{
    [Authorize]
    public class CursoController : Controller
    {
        private IService Service { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        public CursoController(IService _service, UserManager<ApplicationUser> userManager)
        {
            Service = _service;
            UserManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IList<Curso> cursos = await Service.GetAllCourses();
            return View(cursos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Curso curso = await Service.GetCursoPorId(id);
            if (curso == null)
                return NotFound();
            return View(curso);
        }

        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> Create()
        {
            var profesores = await Service.GetAllProfesores();
            ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> Create(CursoCrearViewModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.ProfesorId))
            {
                if (string.IsNullOrEmpty(model.ProfesorId))
                {
                    ModelState.AddModelError("ProfesorId", "Debe seleccionar un profesor.");
                }

                var profesores = await Service.GetAllProfesores();
                ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto");

                return View(model);
            }

            var curso = new Curso
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                ProfesorId = model.ProfesorId,
                FechaCreacion = DateTime.Now
            };

            await Service.CrearCurso(curso);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> Edit(int id)
        {
            var curso = await Service.GetCursoPorId(id);
            if (curso == null)
                return NotFound();

            var profesores = await Service.GetAllProfesores();

            ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto");

            var model = new CursoEditarViewModel
            {
                Id = curso.Id,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                ProfesorId = curso.ProfesorId,
                FechaCreacion = curso.FechaCreacion
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> Edit(CursoEditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var profesores = await Service.GetAllProfesores();
                ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto");
                return View(model);
            }

            var curso = await Service.GetCursoPorId(model.Id);
            if (curso == null)
                return NotFound();

            curso.Nombre = model.Nombre;
            curso.Descripcion = model.Descripcion;
            curso.ProfesorId = model.ProfesorId;

            await Service.EditarCurso(curso);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await Service.GetCursoPorId(id);
            if (curso == null)
                return NotFound();
            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Service.EliminarCurso(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
