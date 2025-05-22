using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcTemplate.Models.ViewModels.AdminUsuarios;

namespace MvcTemplate.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminUsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> LimpiarRolesAdmin()
        {
            var adminUser = await _userManager.FindByEmailAsync("admin@pow.com");
            if (adminUser == null) return NotFound("Usuario admin no encontrado");

            var rolesActuales = await _userManager.GetRolesAsync(adminUser);
            await _userManager.RemoveFromRolesAsync(adminUser, rolesActuales);
            await _userManager.AddToRoleAsync(adminUser, "Administrador");

            return Content("Roles actualizados para admin@pow.com");
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = _userManager.Users.ToList();
            var vista = new List<UsuarioConRolesViewModel>();

            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                vista.Add(new UsuarioConRolesViewModel
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Roles = roles.ToList()
                });
            }

            return View(vista);
        }

        public async Task<IActionResult> EditarRoles(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return NotFound();

            var rolesPermitidos = new List<string> { "Estudiante", "Profesor", "Administrador" };

            var roles = _roleManager.Roles
                .Where(r => rolesPermitidos.Contains(r.Name))
                .Select(r => r.Name)
                .ToList();

            var rolesUsuario = await _userManager.GetRolesAsync(usuario);

            var modelo = new EditarRolesViewModel
            {
                UsuarioId = usuario.Id,
                Email = usuario.Email,
                Roles = roles.Select(rol => new RolSeleccionado
                {
                    Nombre = rol,
                    Seleccionado = rolesUsuario.Contains(rol)
                }).ToList()
            };

            return View(modelo);
        }


        [HttpPost]
        public async Task<IActionResult> EditarRoles(EditarRolesViewModel modelo)
        {
            var usuario = await _userManager.FindByIdAsync(modelo.UsuarioId);
            if (usuario == null) return NotFound();

            var rolesActuales = await _userManager.GetRolesAsync(usuario);
            var resultado = await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);
            if (!resultado.Succeeded) return BadRequest();

            var rolesSeleccionados = modelo.Roles.Where(r => r.Seleccionado).Select(r => r.Nombre);
            await _userManager.AddToRolesAsync(usuario, rolesSeleccionados);

            return RedirectToAction("Index");
        }
    }
}
