using RouteG04.BLL.DTOS.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking);
        EmployeeDetailsDto? GetEmployeeId(int id);
        int CreateEmployee(CreatedEmployeeDto departmentDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
