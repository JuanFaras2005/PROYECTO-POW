using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.IServices;

namespace MvcTemplate.Controllers
{
    [Authorize]
    public class CursoController : Controller
    {
        private IService Service { get; set; }

        public CursoController(IService _service)
        {
            Service = _service;
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

        public async Task<IActionResult> Create()
        {
            var profesores = await Service.GetAllProfesores();
            ViewBag.Profesores = new SelectList(profesores, "Id", "NombreCompleto"); // Ajusta el nombre si es Nombre o NombreCompleto
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                await Service.CrearCurso(curso);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Profesores = await Service.GetAllProfesores();
            return View(curso);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var curso = await Service.GetCursoPorId(id);
            if (curso == null)
                return NotFound();

            ViewBag.Profesores = await Service.GetAllProfesores();
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Curso curso)
        {
            if (ModelState.IsValid)
            {
                await Service.EditarCurso(curso);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Profesores = await Service.GetAllProfesores();
            return View(curso);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var curso = await Service.GetCursoPorId(id);
            if (curso == null)
                return NotFound();
            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Service.EliminarCurso(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
