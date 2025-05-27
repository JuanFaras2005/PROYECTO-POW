namespace MvcTemplate.Models.ViewModels.AdminUsuarios
{
    public class EditarRolesViewModel
    {
        public string UsuarioId { get; set; }
        public string Email { get; set; }
        public List<RolSeleccionado> Roles { get; set; }
    }

    public class RolSeleccionado
    {
        public string Nombre { get; set; }
        public bool Seleccionado { get; set; }
    }
}
