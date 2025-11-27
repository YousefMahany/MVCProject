using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.DTOS.Employee;
using RouteG04.BLL.DTOS.User;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.DAL.Models.Shared;
using RouteG04.DAL.Models.UserModule;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Classes
{
    public class UserService(IUnitOfWork unitOfWork, IMapper mapper ,UserManager<ApplicationUser> userManager) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        //public int CreateUser(CreatedUserDto userDto)
        //{
        //    var User = _mapper.Map<CreatedUserDto, ApplicationUser>(userDto);
        //    _unitOfWork.UserRepository.Add(User);
        //    return _unitOfWork.SaveChanges();
        //}
        public async Task<bool> CreateUser(CreatedUserDto userDto)
        {
            var user = new ApplicationUser
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
            };

            // 1) Create User with password
            var createResult = await _userManager.CreateAsync(user, userDto.Password);
            if (!createResult.Succeeded)
                return false;

            // 2) Add Roles
            if (userDto.Roles != null && userDto.Roles.Any())
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
                if (!roleResult.Succeeded)
                    return false;
            }

            return true;
        }


        public bool DeleteUser(string id)
        {
            var User = _unitOfWork.UserRepository.GetById(id);
            if (User is null) return false;
            else
            {
                _unitOfWork.UserRepository.Update(User);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        public IEnumerable<UserDto> GetAllUsers(bool WithTracking)
        {
            var Users = _unitOfWork.UserRepository.GetAll(WithTracking);
            //var UsersDto = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDto>>(Users);
            var UsersDto = Users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault() ?? ""
            }).ToList();
            return UsersDto;
        }

        public UserDetailsDto? GetUserById(string id)
        {
            var User = _unitOfWork.UserRepository.GetById(id);
            return User is null ? null : _mapper.Map<ApplicationUser, UserDetailsDto>(User);
        }

        public int UpdateUser(UpdatedUserDto userDto)
        {
            _unitOfWork.UserRepository.Update(_mapper.Map<UpdatedUserDto, ApplicationUser>(userDto));
            return _unitOfWork.SaveChanges();
        }
    }
}
