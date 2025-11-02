using AutoMapper;
using RouteG04.BLL.DTOS;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.DTOS.Employee;
using RouteG04.DAL.Models.DepartmentModule;
using RouteG04.DAL.Models.EmployeeModule;
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
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(d => d.EmpGender, options => options.MapFrom(src => src.Gender))
                .ForMember(d => d.EmployeeType, options => options.MapFrom(src => src.EmployeeType));
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

        }
    }
}
