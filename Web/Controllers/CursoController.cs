using Microsoft.AspNetCore.Mvc;
using Domain;
using Services.IServices;

namespace MvcTemplate.Controllers
{
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
            return View(curso);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var curso = await Service.GetCursoPorId(id);
            return View(curso);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var curso = await Service.GetCursoPorId(id);
            return View(curso);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                await Service.CrearCurso(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }
    }
}
