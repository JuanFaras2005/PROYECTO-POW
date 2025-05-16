using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcTemplate.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Controllers
{
    public class AdminProfesoresController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminProfesoresController(UserManager<ApplicationUser> userManager,
                                         RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // LISTADO
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _userManager.GetUsersInRoleAsync("Profesor");

            var lista = usuarios.Select(u => new ProfesorViewModel
            {
                Id = u.Id,
                NombreCompleto = u.Nombre,
                Email = u.Email
            }).ToList();

            return View(lista);
        }

        // CREAR
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProfesorCrearViewModel model)
        {
            if (ModelState.IsValid)
            {
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
                    if (!await _roleManager.RoleExistsAsync("Profesor"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Profesor"));
                    }

                    await _userManager.AddToRoleAsync(user, "Profesor");
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // EDITAR
        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
                return NotFound();

            var model = new ProfesorEditarViewModel
            {
                Id = usuario.Id,
                NombreCompleto = usuario.Nombre,
                Email = usuario.Email
                // Password no se muestra
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProfesorEditarViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _userManager.FindByIdAsync(model.Id);
            if (usuario == null)
                return NotFound();

            usuario.Nombre = model.NombreCompleto;
            usuario.Email = model.Email;
            usuario.UserName = model.Email;

            var result = await _userManager.UpdateAsync(usuario);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            // Si se ingresó una nueva contraseña
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var passwordResult = await _userManager.ResetPasswordAsync(usuario, token, model.Password);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }

        // ELIMINAR
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Eliminar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
                return NotFound();

            await _userManager.DeleteAsync(usuario);

            return RedirectToAction("Index");
        }
    }
}
