using AutoMapper;
using RouteG04.BLL.DTOS;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.DTOS.Employee;
using RouteG04.BLL.DTOS.User;
using RouteG04.DAL.Models.DepartmentModule;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            #region Employee Mapping
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest=>dest.Department,options=>options.MapFrom(src=>src.Department!=null?src.Department.Name:null));
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(d => d.EmpGender, options => options.MapFrom(src => src.Gender))
                .ForMember(d => d.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Department, options => options.MapFrom(src => src.Department != null ? src.Department.Name : null))
                 .ForMember(d => d.Image, options => options.MapFrom(src => src.ImageName));
            CreateMap<CreatedEmployeeDto, Employee>();
            CreateMap<UpdatedEmployeeDto, Employee>();

            #endregion
            #region Department Mapping
            CreateMap<Department, DepartmentsDto>()
           .ForMember(dest => dest.DeptId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.DateOfCreation, opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<Department, DepartmentDetailsDto>()
                .ForMember(dest => dest.DeptId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DateOfCreation, opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<CreatedDepartmentDto, Department>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.DateOfCreation));

            CreateMap<UpdatedDepartmentDto, Department>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.DateOfCreation));
            #endregion
            #region User Mapping
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Role, opt => opt.Ignore()); // هتعبيها باليد بعدين

            CreateMap<CreatedUserDto, ApplicationUser>();
            CreateMap<UpdatedUserDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDetailsDto>();
            #endregion

        }
    }
}
