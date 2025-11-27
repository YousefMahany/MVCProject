using Microsoft.AspNetCore.Mvc.Rendering;

namespace RouteG04.PL.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Phone Number")]
       
        public string PhoneNumber { get; set; } = null!;
        [Required,DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Roles")]
        public string Roles { get; set; } = null!;

        public IEnumerable<SelectListItem> RolesList { get; set; } = new List<SelectListItem>();


    }
}
