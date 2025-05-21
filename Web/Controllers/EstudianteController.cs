using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcTemplate.Models.ViewModels.Estudiantes;
using System.Linq;
using System.Threading.Tasks;

namespace POWApp.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EstudiantesController(UserManager<ApplicationUser> userManager,
                                     RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // LISTAR ESTUDIANTES
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var estudiantes = await _userManager.GetUsersInRoleAsync("Estudiante");

            var lista = estudiantes.Select(e => new EstudianteViewModel
            {
                Id = e.Id,
                Nombre = e.Nombre + " " + e.Apellido,
                Email = e.Email
            }).ToList();

            return View(lista);
        }

        // CREAR ESTUDIANTE
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EstudianteCrearViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.Nombre,
                Apellido = model.Apellido
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Aseguramos que el rol "Estudiante" exista
                if (!await _roleManager.RoleExistsAsync("Estudiante"))
                    await _roleManager.CreateAsync(new IdentityRole("Estudiante"));

                await _userManager.AddToRoleAsync(user, "Estudiante");
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // EDITAR ESTUDIANTE
        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var estudiante = await _userManager.FindByIdAsync(id);
            if (estudiante == null)
                return NotFound();

            var model = new EstudianteEditarViewModel
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Email = estudiante.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EstudianteEditarViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var estudiante = await _userManager.FindByIdAsync(model.Id);
            if (estudiante == null)
                return NotFound();

            estudiante.Nombre = model.Nombre;
            estudiante.Email = model.Email;
            estudiante.UserName = model.Email;

            var result = await _userManager.UpdateAsync(estudiante);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            // Cambiar contraseña si se ingresó una nueva
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(estudiante);
                var passwordResult = await _userManager.ResetPasswordAsync(estudiante, token, model.Password);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }

        // ELIMINAR ESTUDIANTE
        [HttpPost]
        public async Task<IActionResult> Eliminar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var estudiante = await _userManager.FindByIdAsync(id);
            if (estudiante == null)
                return NotFound();

            await _userManager.DeleteAsync(estudiante);
            return RedirectToAction("Index");
        }
    }
}