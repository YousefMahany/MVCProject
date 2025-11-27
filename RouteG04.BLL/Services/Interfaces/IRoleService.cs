using Microsoft.AspNetCore.Identity;
using RouteG04.BLL.DTOS.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Interfaces
{
    public interface IRoleService
    {

        IEnumerable<RoleDto> GetAllRoles(bool WithTracking);
        RoleDto? GetRoleById(string id);
        int CreateRole(CreateRoleDto roleDto);
        int UpdateRole(UpdateRoleDto roleDto);
        bool DeleteRole(string id);
    }
}
