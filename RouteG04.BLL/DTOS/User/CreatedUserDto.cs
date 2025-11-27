using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.DTOS.User
{
    public class CreatedUserDto
    {

        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

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
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Roles")]
        public List<string> Roles { get; set; } = null!;
        public IEnumerable<string> RolesList { get; set; }
    }
}
