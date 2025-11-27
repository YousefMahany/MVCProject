using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.DTOS.Role
{
    public class CreateRoleDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
