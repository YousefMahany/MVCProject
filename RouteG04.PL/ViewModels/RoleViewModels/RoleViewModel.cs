namespace RouteG04.PL.ViewModels.RoleViewModels
{
    public class RoleViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
