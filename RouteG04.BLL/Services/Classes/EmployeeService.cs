using AutoMapper;
using RouteG04.BLL.DTOS.Employee;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Classes
{
    public class EmployeeService( IEmployeeRepository employeeRepository,IMapper mapper) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;

        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking =false)
        {
            var Employees = _employeeRepository.GetAll(WithTracking);
            var EmployeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);
            return EmployeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeId(int id)
        {
            var Employee = _employeeRepository.GetById(id);
           return Employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(Employee);
        }
        public int CreateEmployee(CreatedEmployeeDto departmentDto)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(departmentDto);
            return _employeeRepository.Add(Employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
        }
        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            if (Employee is null) return false;
            else
            {
                Employee.IsDeleted = true;
                return _employeeRepository.Update(Employee) > 0 ? true : false;
            }
        }




    }
}
