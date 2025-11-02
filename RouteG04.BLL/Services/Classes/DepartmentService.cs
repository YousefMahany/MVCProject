using AutoMapper;
using RouteG04.BLL.DTOS;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.Factories;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.DepartmentModule;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Classes
{
    public class DepartmentService(IDepartmentRepository departmentRepository,IMapper mapper) : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        private readonly IMapper _mapper = mapper;

        //Get All
        public IEnumerable<DepartmentsDto> GetAllDepartments()
        {
            var Departments = _departmentRepository.GetAll();
            //return Departments.Select(D => D.ToDepartmentDto());
            var DepartmentDto = _mapper.Map<IEnumerable<Department>, IEnumerable< DepartmentsDto >> (Departments);
            return DepartmentDto;
        }
        //Get By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var Department = _departmentRepository.GetById(id);
            return Department is null ? null : _mapper.Map<Department, DepartmentDetailsDto> (Department);

        }

        //Add
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var Department = _mapper.Map<CreatedDepartmentDto,Department>(departmentDto);
            return _departmentRepository.Add(Department);
        }
        //Update
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(_mapper.Map<UpdatedDepartmentDto,Department>(departmentDto));
        }

        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                Department.IsDeleted = true;
                return _departmentRepository.Update(Department) > 0 ? true : false;
            }
        }

    }
}
