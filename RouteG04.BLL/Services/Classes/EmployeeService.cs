using RouteG04.BLL.DTOS.Employee;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking =false)
        {
            var Employees = _employeeRepository.GetAll(WithTracking);
            var EmployeeDto = Employees.Select(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Email = E.Email,
                IsActive = E.IsActive,
                Salary = E.Salary,
                EmployeeType = E.EmployeeType.ToString()
            });
            return EmployeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeId(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            return Employee is null ? null : new EmployeeDetailsDto()
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Email = Employee.Email,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
                EmployeeType = Employee.EmployeeType.ToString(),
                HiringDate = DateOnly.FromDateTime(Employee.HiringDate),
                PhoneNumber = Employee.PhoneNumber,
                EmpGender = Employee.Gender.ToString(),
                CreatedBy = 1,
                CreatedOn = Employee.CreatedOn,
                LastModifiedBy = 1,
                LastModifiedOn = Employee.LastModificationOn
            };
        }
        public int CreateEmployee(CreatedDepartmentDto departmentDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

       

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
