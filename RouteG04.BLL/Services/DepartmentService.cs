using RouteG04.BLL.DTOS;
using RouteG04.BLL.Factories;
using RouteG04.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        //Get All
        public IEnumerable<DepartmentsDto> GetAllDepartments()
        {
            var Departments = _departmentRepository.GetAll();
            return Departments.Select(D => D.ToDepartmentDto());
        }
        //Get By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var Department = _departmentRepository.GetById(id);
            return Department is null ? null : Department.ToDepartmentDetailsDto();

        }

        //Add
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var Department = departmentDto.ToEntity();
            return _departmentRepository.Add(Department);
        }
        //Update
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                int Result = _departmentRepository.Delete(Department);
                return Result > 0 ? true : false;
            }
        }

    }
}
