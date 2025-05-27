using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcTemplate.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTemplate.Controllers
{
    [Authorize(Roles = "Administrador")]
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _userManager.GetUsersInRoleAsync("Profesor");

            var lista = usuarios.Select(u => new ProfesorViewModel
            {
                Id = u.Id,
                NombreCompleto = u.Nombre + " " + u.Apellido,
                Email = u.Email,
                Rol = "Profesor"
            }).ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProfesorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.NombreCompleto.Split(' ')[0],
                Apellido = model.NombreCompleto.Contains(' ') ? model.NombreCompleto.Substring(model.NombreCompleto.IndexOf(' ') + 1) : ""
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Profesor"))
                    await _roleManager.CreateAsync(new IdentityRole("Profesor"));

                await _userManager.AddToRoleAsync(user, "Profesor");

                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
                return NotFound();

            var model = new ProfesorViewModel
            {
                Id = usuario.Id,
                NombreCompleto = usuario.Nombre + " " + usuario.Apellido,
                Email = usuario.Email
                // No mostramos contraseña en edición
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProfesorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _userManager.FindByIdAsync(model.Id);
            if (usuario == null)
                return NotFound();

            usuario.Nombre = model.NombreCompleto.Split(' ')[0];
            usuario.Apellido = model.NombreCompleto.Contains(' ') ? model.NombreCompleto.Substring(model.NombreCompleto.IndexOf(' ') + 1) : "";
            usuario.Email = model.Email;
            usuario.UserName = model.Email;

            var result = await _userManager.UpdateAsync(usuario);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

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
