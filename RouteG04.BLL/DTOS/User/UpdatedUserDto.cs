using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.DTOS.User
{
    public class UpdatedUserDto
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        public string Role { get; set; } = null!;

    }
}
