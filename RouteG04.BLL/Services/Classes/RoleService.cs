using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RouteG04.BLL.DTOS.Role;
using RouteG04.BLL.DTOS.User;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.Shared;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Classes
{
    public class RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper,IUnitOfWork unitOfWork) : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public int CreateRole(CreateRoleDto roleDto)
        {
            var role = new IdentityRole { Name = roleDto.Name };
            _unitOfWork.RoleRepository.Add(role);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteRole(string id)
        {
            var Role = _roleManager.FindByIdAsync(id).Result;
            if (Role is null) return false;
            else
            {
                _unitOfWork.RoleRepository.Update(Role);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        public IEnumerable<RoleDto> GetAllRoles(bool WithTracking)
        {
            var Roles = _unitOfWork.RoleRepository.GetAll(false);
            var RolesDto = Roles.Select(u => new RoleDto
            {
                Id = u.Id,
                Name = u.Name,
            }).ToList();
            return RolesDto;
        }

        public RoleDto? GetRoleById(string id)
        {
            var Role = _unitOfWork.RoleRepository.GetById(id);
            return Role is null ? null : _mapper.Map<IdentityRole, RoleDto>(Role);
        }

        public int UpdateRole(UpdateRoleDto roleDto)
        {
            _unitOfWork.RoleRepository.Update(_mapper.Map<UpdateRoleDto, IdentityRole>(roleDto));
            return _unitOfWork.SaveChanges();
        }
    }
}
