namespace MvcTemplate.Models.ViewModels.AdminUsuarios
{
    public class UsuarioConRolesViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
