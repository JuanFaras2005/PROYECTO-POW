using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dtos;
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
        public async Task<IActionResult> Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                await Service.CrearCurso(curso);
                return RedirectToAction(nameof(Index));
            }

            var profesores = await Service.GetAllProfesores();
            ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto");
            return View(curso);
        }

        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> Edit(int id)
        {
            var curso = await Service.GetCursoPorId(id);
            if (curso == null)
                return NotFound();

            var profesores = await Service.GetAllProfesores();
            ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto");
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Profesor")]
        public async Task<IActionResult> Edit(Curso curso)
        {
            if (ModelState.IsValid)
            {
                await Service.EditarCurso(curso);
                return RedirectToAction(nameof(Index));
            }

            var profesores = await Service.GetAllProfesores();
            ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto");
            return View(curso);
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

        // Inscripción por estudiante
        [Authorize(Roles = "Estudiante")]
        public async Task<IActionResult> Inscribirse(int id)
        {
            var userId = UserManager.GetUserId(User);
            bool yaInscrito = await Service.EstaInscrito(userId, id);

            if (!yaInscrito)
            {
                await Service.InscribirEstudiante(userId, id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
